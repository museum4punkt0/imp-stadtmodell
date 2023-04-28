using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEditorCamera : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject editorCamera;
    // Start is called before the first frame update

    private void Start()
    {
#if UNITY_EDITOR
        xrOrigin.SetActive(false);
        editorCamera.SetActive(true);
#else
        xrOrigin.SetActive(true);
        editorCamera.SetActive(false);
#endif
    }
    public void ToggleCamera()
    {
        xrOrigin.SetActive(!xrOrigin.activeSelf);
        editorCamera.SetActive(!xrOrigin.activeSelf);
    }
}
