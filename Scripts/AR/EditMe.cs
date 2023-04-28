using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
public class EditMe : MonoBehaviour
{
    
    void Start()
    {
        RuntimeTransformer.Instance.targetTransform = transform;
   
    }


}
