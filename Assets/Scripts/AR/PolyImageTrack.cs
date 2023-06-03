using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;

public class PolyImageTrack : MonoBehaviour
{
    public static PolyImageTrack GiessenGrad;
    public ARPlane passerX;
    public Transform passerXFinder;
    public ARPlane passerZ;
    public Transform passerZFinder;

    public bool justShow;
    public GameObject mainContentAllObjects;
    public Vector3 currentVelocityPos;
    public float currentVelocityRot;
    public Text trackDebug;
    public List<PolyImageSubTracker> SubTrackers;
    private bool trackingInProgress;
    public float lastChangeTime;
    public bool everFound = false;
    public bool roomReady = false;
    public Vector3  lastForward;
    public bool ignoreThisFrame;
    public bool ignoreThisFrameForRaum;
    public Transform roomOrienter;
    public List<Vector3> positions = new List<Vector3>();
    public List<Vector3> forwards = new List<Vector3>();
    public List<Vector3> ups = new List<Vector3>();
    public Vector3 avgPos;
    public Vector3 avgFwd;
    public Vector3 avgUp;
    public bool debugColors;
    public bool upCheck = true;
    public bool flatcheck = true;
    public bool isGiessenModel;


    private void Awake()
    {
        if (!justShow )
        mainContentAllObjects.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        lastChangeTime = -2;
        if (isGiessenModel)
            GiessenGrad = this;
    }

    // Update is called once per frame
    void Update()
    {
        ignoreThisFrame = false;
        ignoreThisFrameForRaum = true;
        Quaternion averageRotation = new Quaternion(0, 0, 0, 0);
        Vector3 averagePosition = new Vector3();
        int amount = 0;
        string debugLog = "INFLUENCES:";
        if (true)
        { 
        foreach (var subTracker in SubTrackers)
        {
            if (subTracker.gameObject.activeInHierarchy)
            {
                if (subTracker.State == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                {
                    if (subTracker.goodForRaum)
                    {
                        ignoreThisFrameForRaum = false;
                    }
                    lastChangeTime = Time.time;
                    amount++;
                    var trackRot = subTracker.transform.rotation;
                    var trackPos = subTracker.transform.position;
                    averageRotation = Quaternion.Slerp(averageRotation, trackRot, 1f / amount);
                    averagePosition = Vector3.Lerp(averagePosition, trackPos, 1f / amount);
                    
                    var myLog = "\n#: " + subTracker.gameObject.name + " ";
                    myLog += trackPos + " : " + trackRot.eulerAngles;
                    debugLog += myLog;

                        if (!everFound)
                        {
                            everFound = true;
                            transform.parent = null;
                            lastForward = subTracker.transform.forward;
                        }
                        else
                        {
                            var angleChange = Vector3.Angle(lastForward, subTracker.transform.forward);

                            if (angleChange > 5)
                            {
                                ignoreThisFrame = true;

                                if (debugColors)
                                {
                                    foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
                                    {
                                        sr.color = Color.red;
                                    }
                                }
                            }
                            else
                            {
                                if (debugColors)
                                {
                                    foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
                                    {
                                        sr.color = Color.white;
                                    }
                                }
                                lastForward = subTracker.transform.forward;
                            }

                            // QUICK FIX
                            lastForward = subTracker.transform.forward;
                        }
                        debugLog = "AP! "+subTracker.transform.up + "\n FWD: " + subTracker.transform.forward;

                        if ((Vector3.Angle(Vector3.up, subTracker.transform.forward) > 2 && upCheck) || (Vector3.Angle(Vector3.up,subTracker.transform.up)>2 && flatcheck))
                        {
                            debugLog += "FAIL";
                            if (debugColors)
                            {
                                foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
                                {
                                    sr.color = Color.blue;
                                }
                            }
                            ignoreThisFrame = true;
                        }
                    }
            }
        }
        }
        if (amount > 0 && !ignoreThisFrame )
        {
            mainContentAllObjects.SetActive(true);
            if (!trackingInProgress)
            {
                transform.rotation = averageRotation;
                transform.position = averagePosition;
            }
            else
            { 
            trackingInProgress = true;
            for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
            {
                transform.GetChild(childIndex).gameObject.SetActive(true);
            }
            transform.position = Vector3.SmoothDamp(transform.position, averagePosition, ref currentVelocityPos, 0.3f);
            float delta = Quaternion.Angle(transform.rotation, averageRotation);
            if (delta > 0f)
            {
                float t = Mathf.SmoothDampAngle(delta, 0.0f, ref currentVelocityRot, 0.3f);
                t = 1.0f - (t / delta);
                transform.rotation = Quaternion.Slerp(transform.rotation, averageRotation, t);
            }
                //
            }
            if (roomOrienter!= null && !ignoreThisFrameForRaum)
            {
                roomReady = true;
                avgPos = roomOrienter.transform.position;
                avgFwd = roomOrienter.transform.forward;
                avgUp = roomOrienter.transform.up;
            }
        }

        if (trackDebug != null)
            trackDebug.text = debugLog;

        if (DebugManager.Instance.experimentalFeaturesOn)
        { 
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;

        if (passerX != null && passerZ != null)
        {
            everFound = true;
            transform.parent = null;
            x = transform.position.x - passerXFinder.position.x + passerX.center.x;
            z = transform.position.z - passerZFinder.position.z + passerZ.center.z;
            float minY = 100;
            ARPlane lowplane = null;
            foreach (var arplane in PlaneTrackingModeVisualizer.allPlanes)
            {
                if (minY > arplane.center.y)
                {
                    lowplane = arplane;
                }
                minY = Mathf.Min(minY, arplane.center.y);
            }
            y = minY+0.6f;

            transform.up = lowplane.normal;
        }


        transform.position = new Vector3(x, y, z);
        }
    }

    public void FeedPosRot(Vector3 pos, Quaternion rot)
    {
        transform.rotation = rot;
        transform.position = pos;
        trackingInProgress = true;
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            transform.GetChild(childIndex).gameObject.SetActive(true);
        }
    }
}
