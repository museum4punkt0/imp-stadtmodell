using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampCopier : MonoBehaviour
{
    public List<Transform> railingRoots;
    public GameObject original;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var root in railingRoots)
        {
            var lamp = Instantiate(original, root);
            lamp.transform.localPosition = original.transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
