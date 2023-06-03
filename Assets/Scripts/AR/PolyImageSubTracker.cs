using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class PolyImageSubTracker : MonoBehaviour
{
    public Text debug;
    public bool goodForRaum;
    private TrackingState previousState = TrackingState.None;
    private ARTrackedImage myImage;
    public List<TrackableStateResponder> respoders = new List<TrackableStateResponder>();
    private float xTrackTreshold =0.8f;
    public float viewAngleTolerance = 60f;
    public float roomAngleTolearnce = 15f;
    private float yTrackTreshold = 0.8f;
    public bool FORCEME = false;
        ///public ARTrackedImage myArTrackedImage;
    //public SubTrackerState TrackingState;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        goodForRaum = false;
        if (debug != null)
            debug.text = "";
        if (FORCEME)
        {
            State = TrackingState.Tracking;
            goodForRaum = true;
            return;
        }
        if (myImage == null) State = TrackingState.None;
        else {
         

            Vector2 viewportCoords = Camera.main.WorldToViewportPoint(myImage.transform.position);
        if (viewportCoords.x >= 0.5f - xTrackTreshold / 2 && viewportCoords.x <= 0.5f + xTrackTreshold / 2 && viewportCoords.y >= 0.5f - yTrackTreshold / 2 && viewportCoords.y <= 0.5f + yTrackTreshold / 2)
        {
            var viewAngle = Vector3.Angle(Camera.main.transform.forward, -transform.up);
            if (debug != null)
                debug.text = ">" + viewAngle;
            if (viewAngle < viewAngleTolerance) {
                    if (viewAngle > roomAngleTolearnce)
                    {
                        goodForRaum = true;
                    }
                    State = myImage.trackingState;
                }   
                else
                    State = TrackingState.None;
        }
        else { 
        State = TrackingState.None;
        }
        }
        if (myImage == null)
            myImage = GetComponentInParent<ARTrackedImage>();

        if (State != previousState)
        {
            foreach (var tsr in respoders)
            {
                tsr.StateChanged(State);
            }
        }
        previousState = State;
    }

    public TrackingState State;
}