using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARFoundation.Samples;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance;
    public ARPlaneManager planeManager;
    public ARPointCloudManager pointCloudManager;
    public ARWorldMapController arWorldMapController;
    public ARSession arSession;
    public bool waitingForWorldMap;
    public bool worldMapLoaded;
    
    public bool worldMapReady
    {
        get
        {
            return worldMapLoaded && arWorldMapController.arSession.subsystem.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking;
        }
    }

    private Color disabledColor;
    public Image lamp1;
    public Image lamp2;


    public void RestartScene()
    {
        Time.timeScale = 1;
        PlaneTrackingModeVisualizer.mioFound = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        Instance = this;
        disabledColor = lamp1.color;
        Invoke("LoadWorldMap", 0.1f);
    }

    public void LoadWorldMap()
    {
        arWorldMapController.LoadWorldmap();
    }

    private void Update()
    {
        lamp1.color = worldMapLoaded ? Color.green : disabledColor;
        lamp2.color = worldMapReady ? Color.green : disabledColor;
    }
    public void StartPlayMode()
    {
        planeManager.enabled = true;
        pointCloudManager.enabled = true;
        arWorldMapController.enabled = true;
        arWorldMapController.OnLoadButton();
    }
    public void StartSetupMode()
    {
        planeManager.enabled = true;
        pointCloudManager.enabled = true;
        arWorldMapController.enabled = true;
    }

}
