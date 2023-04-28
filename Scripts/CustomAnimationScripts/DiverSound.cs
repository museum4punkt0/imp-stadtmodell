using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverSound : MonoBehaviour
{

    public List<AudioClip> splash;
    public void PlaySplash()
    {
        var clip = splash[Random.Range(0, splash.Count)];
        AudioSource.PlayClipAtPoint(clip, transform.position, Random.Range(0.8f, 1f));
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
