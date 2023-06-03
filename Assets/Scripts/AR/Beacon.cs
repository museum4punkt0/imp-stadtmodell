using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    private float treshold = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Scale((Camera.main.transform.position-transform.position),new Vector3(1,0,1)).magnitude < treshold && BeaconManager.Instance.ActiveBeacon != this)
        {
            BeaconManager.Instance.PlayBeacon(this);
        }
    }
}
