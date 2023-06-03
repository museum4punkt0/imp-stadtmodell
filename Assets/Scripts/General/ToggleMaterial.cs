using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMaterial : MonoBehaviour
{
    public GameObject Occluder;
    public GameObject Model;
    public int state = 0;

    public void ToggleMat()
    {
        state++;
        if (state == 3)
            state = 0;

        switch (state)
        {
            case 0:
                Occluder.SetActive(true);
                Model.SetActive(false);
                break;

            case 1:
                Occluder.SetActive(false);
                Model.SetActive(true);
                break;

            case 2:
                Occluder.SetActive(false);
                Model.SetActive(false);
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
