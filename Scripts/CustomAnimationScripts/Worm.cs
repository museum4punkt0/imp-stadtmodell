using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public GameObject root;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Umbrella", WormSpawnwer.Instance.umbrellaTime);
    }

    public void Crawled()
    {
        animator.SetInteger("Crawl", animator.GetInteger("Crawl") - 1);
    }

    public void DestoryMyAll()
    {
        Destroy(root);
    }
}
