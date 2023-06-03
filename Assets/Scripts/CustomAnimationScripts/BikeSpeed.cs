using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSpeed : MonoBehaviour
{
    public float speedMultiplier;
    public Animator animator;
    public float animationTime = 0;
    private Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceCovered = (transform.position - lastPos).magnitude;
        animationTime += distanceCovered * speedMultiplier;
        animator.SetFloat("Movement", animationTime);
        lastPos = transform.position;
    }
}
