using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSpawnwer : MonoBehaviour
{
    public float nextWormSpawn;
    public float minNextWormDelay;
    public float maxNextWormDelay;
    public bool umbrellaTime;
    public GameObject wormPrefab;
    public bool spawning;
    public static WormSpawnwer Instance;
    public void SetUmbrellaTime(bool b)
    {
        umbrellaTime = b;
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning)
        { 
        if (Time.time>=nextWormSpawn)
        {
            var next = Instantiate(wormPrefab, transform);
            var randomSpin = Random.Range(-160f, 0f);
            next.transform.localRotation = Quaternion.Euler(0, 0, randomSpin);
            next.transform.localPosition = Vector3.forward * Random.Range(-0.05f, 0.1f);
            nextWormSpawn = Time.time+ Random.Range(minNextWormDelay, maxNextWormDelay);
            next.GetComponentInChildren<Animator>().SetInteger("Crawl", Random.Range(1, 3));
        }
        }
    }

    public void StartSpawn()
    {
        spawning = true;
        nextWormSpawn = Time.time;
    }

    public void EndSpawn()
    { spawning = false;
    }

}
