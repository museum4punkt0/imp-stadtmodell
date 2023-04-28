using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class TrackableStateResponder : MonoBehaviour
{
    public virtual void StateChanged(TrackingState state) { }
}
