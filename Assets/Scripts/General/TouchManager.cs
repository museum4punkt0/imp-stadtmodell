using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    public List<Tap> taps = new List<Tap>();
    public List<Tap> debugTaps = new List<Tap>();
    public List<Tap> resetTaps = new List<Tap>();
    public Animator resetBox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var newTouch = false;
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && !newTouch)
            {
                newTouch = true;
                var newTap = new Tap()
                {
                    viewportPos = Camera.main.ScreenToViewportPoint(touch.position),
                    timestamp = Time.time
                };
               
                taps.Add(newTap);
                if (newTap.viewportPos.x > 0.8f && newTap.viewportPos.y > 0.8f)
                    debugTaps.Add(newTap);
                else
                    resetTaps.Add(newTap);
                if (resetTaps.Count >= 4)
                {
                    resetTaps.Clear();
                    resetBox.SetTrigger("Toggle");
                }

                if (debugTaps.Count >= 6)
                {
                    debugTaps.Clear();
                    DebugManager.Instance.Toggle();
                }
            }
        }

        for (int i = 0; i < taps.Count; i++)
        {
            if (Time.time - taps[i].timestamp > 6)
            {
                taps.Remove(taps[i]);
                i--;
            }
        }

        for (int i = 0; i < debugTaps.Count; i++)
        {
            if (Time.time - debugTaps[i].timestamp > 6)
            {
                debugTaps.Remove(debugTaps[i]);
                i--;
            }
        }
        for (int i = 0; i < resetTaps.Count; i++)
        {
            if (Time.time - resetTaps[i].timestamp > 4)
            {
                resetTaps.Remove(resetTaps[i]);
                i--;
            }
        }
    }

    public class Tap
    {
        public Vector2 viewportPos;
        public float timestamp;
    }
}
