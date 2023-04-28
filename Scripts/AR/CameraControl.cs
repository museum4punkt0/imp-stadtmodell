using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera me;
    public Camera master;
    public LayerMask myMask;
    public RenderTexture retex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        me.CopyFrom(master);
        me.transform.localPosition = Vector3.zero;
        me.transform.localRotation = Quaternion.identity;
        me.targetTexture = retex;
        me.cullingMask = myMask;
        me.clearFlags = CameraClearFlags.SolidColor;
    }
}
