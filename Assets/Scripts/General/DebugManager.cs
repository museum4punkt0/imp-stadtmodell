using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;
    public ARPlaneManager planaManager;
    public GameObject allUI;
    public bool OverrideAlwaysShow;
    public UnityEngine.UI.Toggle experimentalFeaturesToggle;

    public bool experimentalFeaturesOn;


    public void SetExperimental (bool b)
    {
        experimentalFeaturesOn = b;
        PlayerPrefs.SetInt("ExperimentalFeatures", b ? 1 : 0);
    }

    private void Start()
    {

        Instance = this;
        var showDebugonStart = OverrideAlwaysShow || (PlayerPrefs.HasKey("DebugShowOnStart") && PlayerPrefs.GetInt("DebugShowOnStart") == 1);
        experimentalFeaturesOn = (PlayerPrefs.HasKey("ExperimentalFeatures") && PlayerPrefs.GetInt("ExperimentalFeatures") == 1);
        experimentalFeaturesToggle.isOn = experimentalFeaturesOn;
        SetUIActive(showDebugonStart);
        ARWorldMapController.Instance.SetShowTrackables(showDebugonStart);
    }
    public void Toggle()
    {
        allUI.SetActive(!allUI.activeSelf);
    }
    public void SetUIActive(bool b)
    {
        allUI.SetActive(b);
    }

    public void SetShowDebugOnStart()
    { }

    private void Update()
    {
    }

}
