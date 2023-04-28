using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIlboardY : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            Vector3 sameLevel = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
            transform.LookAt(sameLevel, Vector3.up);
        }
    }
}
