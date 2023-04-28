using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LookAtLookFromRun : MonoBehaviour
{
    [System.Serializable]
    public class OnVisibleEvent : UnityEvent { }
   
    public Collider CameraLocationZone;
    public Transform LookAtTarget;
    [SerializeField]
    public OnVisibleEvent onVisibleEvent;
    [SerializeField]
    public OnVisibleEvent onHiddentEvent;
    public bool inView;
    public bool fireOnce;
    public bool fired;
    public List<PlayableManager.SceneState> statesToRunIn;
    public List<PlayableManager.SceneState> statesToRunShowIn;
    public List<PlayableManager.SceneState> statesToRunHideIn;

    // TODO ADD RESTART!
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            if (statesToRunIn == null || statesToRunIn.Count == 0 || statesToRunIn.Contains(PlayableManager.Instance.sceneState))
            {
                Vector2 viewportCoords = Camera.main.WorldToViewportPoint(LookAtTarget.position);
                //Debug.Log(viewportCoords);
                if (!inView && ((CameraLocationZone == null || CameraLocationZone.ClosestPoint(Camera.main.transform.position) == Camera.main.transform.position) && viewportCoords.x >= 0 && viewportCoords.x <= 1 && viewportCoords.y >= 0 && viewportCoords.y <= 1))
                {
                    if (statesToRunShowIn == null || statesToRunShowIn.Count == 0 || statesToRunShowIn.Contains(PlayableManager.Instance.sceneState))
                    {
                        inView = true;
                        if (!fired || !fireOnce)
                        {
                            Debug.Log("GO!");
                            onVisibleEvent.Invoke();
                            fired = true;
                        }
                    }
                }
                else if (inView && !((CameraLocationZone == null || CameraLocationZone.ClosestPoint(Camera.main.transform.position) == Camera.main.transform.position) && viewportCoords.x >= -0.4f && viewportCoords.x <= 1.4f && viewportCoords.y >= -0.4f && viewportCoords.y <= 1.4f))
                {
                    if (statesToRunHideIn == null || statesToRunHideIn.Count == 0 || statesToRunHideIn.Contains(PlayableManager.Instance.sceneState))
                    {
                        inView = false;
                        onHiddentEvent.Invoke();
                    }
                }
            }
        }
    }
}
