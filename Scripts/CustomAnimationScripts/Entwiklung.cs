using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entwiklung : MonoBehaviour
{
    public float unfoldDuration = 0.8f;
    public float secondPerMeterDistanceDelay = 1f;
    public float animationStartTime;
    public bool isUnfolding;
    public bool isFolding;
    public Transform root;
    public bool startFold;
    public bool startUnfold;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, 0);
            child.gameObject.SetActive(false);
        }
    }

    public void StartFold()
    {
        animationStartTime = Time.time;
        isFolding = true;
    }
    public void StartUnfold()
    {
        animationStartTime = Time.time;
        isUnfolding = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (startFold)
        {
            startFold = false;
            StartFold();
        }
        if (startUnfold)
        {
            startUnfold = false;
            StartUnfold();
        }
        if (isUnfolding || isFolding)
        {
            var timeElapsed = Time.time - animationStartTime;
            bool anyleft = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var delay = Mathf.Pow( Vector3.Distance(child.position, root.position),0.5f)* secondPerMeterDistanceDelay;
                var lerp = Mathf.Clamp01((timeElapsed - delay) / unfoldDuration);
                if (lerp < 1) anyleft = true;
                if (isFolding)
                    lerp = 1f - lerp;
                child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, lerp);
                child.gameObject.SetActive(lerp > 0);
            }
            if (!anyleft)
            {
                isUnfolding = false;
                isFolding = false;
            }
        }
        
        
    }
}
