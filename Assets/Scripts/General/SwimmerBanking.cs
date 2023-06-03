using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmerBanking : MonoBehaviour
{
    public Transform helper;
    public Transform banker;
    public Transform main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle =  Mathf.Abs(Vector3.Angle(Camera.main.transform.forward, main.forward * -1));
        if (angle > 180)
            angle = 360 - angle;
        angle = 90 - Mathf.Abs(angle - 90);
           // Debug.Log(angle);
        float bank = Mathf.Lerp(0, 80, Mathf.InverseLerp(0,90,angle));
        if (Vector3.Distance(Camera.main.transform.position, main.position) > Vector3.Distance(Camera.main.transform.position, helper.position))
            bank *= -1;
        banker.localRotation = Quaternion.Euler(new Vector3(bank, 0, 0));
    }
}
