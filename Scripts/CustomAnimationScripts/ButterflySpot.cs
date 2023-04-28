using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpot : MonoBehaviour
{
    public float movementSpeed = 0.2f;
    public float landingProximity = 0.05f;
    public float movementRange = 20f;
    public float flySpeed = 20f;
    public float threshRange = 3f;
    public float landingRange = 0.5f;
    public float targetingSlowdown = 0.75f;
    public float targetingNarrowing = 0.65f;
    public float scale = 1f;
    public ButteflyState stateOnReachTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
