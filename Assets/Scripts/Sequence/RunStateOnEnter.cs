using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
public class RunStateOnEnter : MonoBehaviour
{
    public bool fired;
    public Collider CameraLocationZone;
    public TimelineAsset timeline;
    public PlayableManager.SceneState activeInState;
    public LookAtLookFromRun lookAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetMe()
    {
        fired = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!fired && PlayableManager.Instance.sceneState== activeInState )
        {
            if  (CameraLocationZone.ClosestPoint(Camera.main.transform.position) == Camera.main.transform.position)
            {
                if (lookAt == null || lookAt.inView)
                { 
                fired = true;
                PlayableManager.Instance.PlaySequence(timeline);
                }
            }

        }
    }
}
