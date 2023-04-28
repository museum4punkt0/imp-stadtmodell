using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideThunderstorm : MonoBehaviour
{
    public List<Animator> clouds;
    public List<Animator> cloudsToHide;
    public void EndThunderstorm()
    {
        cloudsToHide = new List<Animator>();
        foreach (var cloud in clouds)
        {
            cloudsToHide.Add(cloud);
            Invoke("HideCloud", Random.Range(0, 3f));
        }
    }

    void HideCloud()
    {
        var randomCloudIndex = Random.Range(0, cloudsToHide.Count);

        cloudsToHide[randomCloudIndex].SetTrigger("Hide");
        cloudsToHide.RemoveAt(randomCloudIndex);
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
