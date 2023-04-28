using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class VolksbadRoot : MonoBehaviour
{
    public GameObject fullMesh;
    public GameObject physicalModel;
    public GameObject netBlock;
    public MeshRenderer physicalMesh;
    public Material modelMat;
    public Material occluderMat;
    public bool showPhysicalMesh;
    public bool waitingForDetection;
    public PolyImageTrack volksbadTracker;
    
    public void ToggleFullMesh()
    {
        fullMesh.SetActive(!fullMesh.activeSelf);
    }

    public void TogglePhysicalModel()
    {
        showPhysicalMesh = !showPhysicalMesh;
        physicalMesh.material = (showPhysicalMesh) ? modelMat : occluderMat;
        //physicalModel.SetActive(!physicalModel.activeSelf);
    }

    public void ToggleNet()
    {
        netBlock.SetActive(!netBlock.activeSelf);
        //physicalModel.SetActive(!physicalModel.activeSelf);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CheckDetectionPause()
    {
        if (!volksbadTracker.everFound)
        {
            PlayableManager.Instance.director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            waitingForDetection = true;
        }     
    }


    // Update is called once per frame
    void Update()
    {
        if (waitingForDetection && volksbadTracker.everFound)
        {
            PlayableManager.Instance.director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }

    }
}
