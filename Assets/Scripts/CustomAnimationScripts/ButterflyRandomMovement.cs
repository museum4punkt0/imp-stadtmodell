using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyRandomMovement : MonoBehaviour
{ 
    public ButterflyMaster master;
    public Animator animator;
    public GameObject bodyAnface;
    public GameObject bodyProfile;
    public Vector3 target;
    public float movementRange = 20f;
    public Vector3 currentVelocity;
    public float flySpeed = 20f;
    public float nextSwitchAverage = 5f;
    public Vector3 accel;
    public float threshRange = 3f;
    public float landingRange = 0.5f;
    public float nextSwitch;
    public float speedOfChange = 0.5f;
    public ButteflyState State = ButteflyState.IdleRoaming;
    public List<Vector3> lastPos = new List<Vector3>();
    public float targetingSlowdown = 0.75f;
    public float targetingNarrowing = 0.65f;

    public ButteflyState stateOnReachTarget = ButteflyState.Landed;

    public void FeedSpot(ButterflySpot spot)
    {
        movementRange = spot.movementRange;
        flySpeed = spot.flySpeed;
        threshRange = spot.threshRange;
        landingRange = spot.landingRange;
        targetingSlowdown = spot.targetingSlowdown;
        targetingNarrowing = spot.targetingNarrowing;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos.Add(transform.position);
        target = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f), Random.Range(-1f, 1f))* movementRange;
        nextSwitch = Time.time + Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == ButteflyState.IdleRoaming || State == ButteflyState.MovingTowardsGoal)
        {
            Vector3 distance = (target - transform.localPosition);
            var speedMultiplier = (State == ButteflyState.IdleRoaming) ? 1 : targetingSlowdown;
            var targetVelocity = distance.normalized * flySpeed * speedMultiplier;
            
            currentVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref accel, speedOfChange);
            transform.localPosition += currentVelocity * Time.deltaTime;
            if (distance.magnitude < threshRange || Time.time > nextSwitch)
            {

                var spread = (State == ButteflyState.IdleRoaming) ? 1 : targetingNarrowing;
                target = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.7f, 0.7f), Random.Range(-1f, 1f)) * movementRange * spread;
                var direction = target - transform.position;
                var trytimes = 0;
                while (Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && Mathf.Abs(direction.y) > Mathf.Abs(direction.z) && trytimes<10)
                {
                    //Debug.Log("rirol!!");
                    trytimes++;
                    target = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.7f, 0.7f), Random.Range(-1f, 1f)) * movementRange;
                    direction = target - transform.position;
                }
                nextSwitch = Time.time + Random.Range(nextSwitchAverage*0.5f, nextSwitchAverage*1.5f);
            }
            //transform.LookAt(transform.parent.TransformPoint(transform.localPosition + currentVelocity));
            transform.LookAt(transform.position + transform.position - lastPos[0]);
        }
        else if (State == ButteflyState.Landing)
        {
            Vector3 distance = (target - transform.localPosition);
            if (distance.magnitude > landingRange)
            {
                var targetVelocity = distance.normalized * flySpeed * targetingSlowdown;
                currentVelocity = targetVelocity;
                transform.localPosition += currentVelocity * Time.deltaTime;
                transform.LookAt(transform.position + transform.position - lastPos[0]);
  //              transform.LookAt(transform.parent.TransformPoint(transform.localPosition + currentVelocity));
            }
            else
            {
                currentVelocity = Vector3.zero;
                transform.rotation = Quaternion.identity;
                State = stateOnReachTarget;
                if (State == ButteflyState.IdleRoaming)
                {
                    master.StartParticles();
                }
            }
        }
        if (Camera.main != null)
        {
            var camSpaceForward = Camera.main.transform.InverseTransformDirection(transform.forward);
            //Debug.Log();
            var profile = Mathf.Abs(camSpaceForward.x) >= Mathf.Abs(camSpaceForward.z);
            bodyAnface.SetActive(!profile);
            bodyProfile.SetActive(profile);
        }
        lastPos.Add (transform.position);
        if (lastPos.Count > 10)
            lastPos.RemoveAt(0);
    }

    public void Land()
    {
        if (stateOnReachTarget == ButteflyState.Landed)
            animator.SetTrigger("Land");
        State = ButteflyState.Landing;
        target = Vector3.zero;
    }
}


public enum ButteflyState
{
    IdleRoaming,
    MovingTowardsGoal,
    Landing,
    Landed,
    TakeOff
}