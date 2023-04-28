using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyActiveInSequence : MonoBehaviour
{
    public List<PlayableManager.SceneState> statesToPlayIn;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target.SetActive(statesToPlayIn.Contains(PlayableManager.Instance.sceneState));
    }
}
