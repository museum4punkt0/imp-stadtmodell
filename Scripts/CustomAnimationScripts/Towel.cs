using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towel : MonoBehaviour
{
    public Animator animator;
    public Transform target;
    public float cycleCount;
    public float travelTime;
    public float loopDuration = 16f / 12;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool isTraveling;
    public float startTime;
    public bool started;
    public bool timeToRun;
    public Transform volksbadRoot;

    public void StartSequence()
    {
        timeToRun = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeToRun)
        {
            timeToRun = false;
            animator.SetTrigger("Run");
        }

        if (isTraveling)
        { 
        var elapsedTime = Time.time - startTime;
        var t = elapsedTime / travelTime;
        if (t >= 1)
        {
            t = 1;
            isTraveling = false;
            ReachEnd();
        }
            var pos = Vector3.Lerp(startPos, endPos, t);
        animator.SetFloat("LoopRunner", t * cycleCount);
        transform.position = pos;
        }
    }

    public void StartMove()
    {
        if (!started)
        {
            started = true;
            startPos = transform.position;
            transform.parent = volksbadRoot;
            endPos = target.position;
            startTime = Time.time;
            isTraveling = true;
        }
    }
    
    public void ReachEnd()
    {
        animator.SetTrigger("Land");
    }

   
}
