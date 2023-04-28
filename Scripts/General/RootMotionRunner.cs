using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionRunner : MonoBehaviour
{
    public List<Transform> points;
    public int nextTargetIndex;
    public float toleranceRadius = 0.05f;
    public JelaDirection direction;
    public float lastDist;
    public bool gotoRandom;
    public Transform customPivot;
    public Transform currentTarget {
    get
        {
            return points[nextTargetIndex];
        }    
    }
    // Start is called before the first frame update
    void Start()
    {
        nextTargetIndex = 1;
        lastDist = (customPivot != null) ? Vector3.Distance(customPivot.position, currentTarget.position) : Vector3.Distance(transform.position, currentTarget.position);
    }

        // Update is called once per frame
        void Update()
    {
        var currentTarget = points[nextTargetIndex];

        UpdateRotation();

            var dist = (customPivot != null) ? Vector3.Distance(customPivot.position, currentTarget.position) : Vector3.Distance(transform.position, currentTarget.position);

        if (dist < toleranceRadius)
        {
            NextTarget();
        } 
        else if (dist > lastDist+0.002f)
        {
            NextTarget();
        }
        else
        {
            lastDist = dist;
        }
    }

    void NextTarget()
    {
        if (gotoRandom)
        {
            var ind = Random.Range(0, points.Count);
            while (ind == nextTargetIndex)
            {
                ind = Random.Range(0, points.Count);
            }
            nextTargetIndex = ind;
        }
        else
        {
            nextTargetIndex++;
            if (nextTargetIndex >= points.Count)
                nextTargetIndex = 0;
        }
        if (customPivot != null)
        {

            lastDist = Vector3.Distance(customPivot.position, currentTarget.position);
        }
        else
        {
            lastDist = Vector3.Distance(transform.position, currentTarget.position);
        }

        UpdateRotation();
	}

    void UpdateRotation()
    {
        var myPos = (customPivot == null) ? transform : customPivot.transform;
        switch (direction)
        {
            case JelaDirection.XPositive:
                transform.right = currentTarget.position - myPos.position;
                break;
            case JelaDirection.XNegative:
                transform.right = -(currentTarget.position - myPos.position);
                break;
        }
        if (customPivot != null)
        { 
        Vector3 glavudzapos = customPivot.position;
        var mojbabo = transform.parent;
        customPivot.parent = null;
        transform.parent = customPivot;
        customPivot.position = glavudzapos;
        transform.parent = mojbabo;
        customPivot.parent = transform;
        }

    }

    public enum JelaDirection
    {
        XPositive,
        XNegative
    }
}
