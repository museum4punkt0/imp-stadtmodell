using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinaBikeUI : MonoBehaviour
{
    public Transform scaler;
    public Vector3 startScale;
    public Animator pathAnimator;
    // Start is called before the first frame update
    void Start()
    {
        startScale = scaler.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        pathAnimator.SetFloat("speed", multiplier);
    }

    public void SetScale(float factor)
    {
        scaler.localScale = startScale * factor;
    }
}
