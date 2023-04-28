using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class StartLina : MonoBehaviour
{
    public TimelineAsset sequence1;
    public PlayableManager playableManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        playableManager.PlaySequence(sequence1);
    }
}
