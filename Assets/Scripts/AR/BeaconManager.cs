using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BeaconManager : MonoBehaviour
{
    public bool SpeakBeacons = true;
    public Beacon ActiveBeacon;
    public static BeaconManager Instance;
    public AudioSource narratorAudioSource;
    public List<AudioClip> clips; 
    public GameObject beaconPrefab;
    public List<GameObject> beacons;
    public ARTargetObjectSpawner arPersistentObjectPositioners;
    public PolyImageTrack gisengrad;
    string path
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, "beacons.json");
        }
    }
    public void SaveBeacons()
    {
        var beaconData = new BeaconsData();
        beaconData.beacons = new List<BeaconPosition>();
        beaconData.persistentObjects = new List<ARPersistentObjectData>();

        foreach (var go in beacons)
        {
            beaconData.beacons.Add(new BeaconPosition() { position = go.transform.position });
        }
        foreach (var pair in arPersistentObjectPositioners.pairs)
        {
            beaconData.persistentObjects.Add(new ARPersistentObjectData() { GameObjectName = pair.gameObject.name, position = pair.gameObject.transform.position, rotation = pair.gameObject.transform.rotation });
        }
        beaconData.gisengrad = new ARPersistentObjectData() { GameObjectName = gisengrad.gameObject.name, position = gisengrad.transform.position, rotation = gisengrad.transform.rotation };
        var json = JsonUtility.ToJson(beaconData);
        File.WriteAllText(path, json);
    }

    public void LoadBeacons()
    {
        var json = File.ReadAllText(path);
        var beaconData = JsonUtility.FromJson<BeaconsData>(json);
        foreach (var beacon in beaconData.beacons)
        {
            var go = Instantiate(beaconPrefab);
            go.transform.position = beacon.position;
            beacons.Add(go);
        }
        foreach (var persistent in beaconData.persistentObjects)
        {
            foreach (var pair in arPersistentObjectPositioners.pairs)
            {
                if (pair.gameObject.name == persistent.GameObjectName)
                {
                    pair.gameObject.transform.position = persistent.position;
                    pair.gameObject.transform.rotation = persistent.rotation;
                    pair.gameObject.SetActive(true);
                    Debug.Log("Acivatin - " + pair.gameObject.name);
                }
             }
        }
        gisengrad.FeedPosRot(beaconData.gisengrad.position, beaconData.gisengrad.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBeacon()
    {
        var newBeacon = Instantiate(beaconPrefab);
        newBeacon.transform.position = Camera.main.transform.position;
        beacons.Add(newBeacon);
    }

    public void PlayBeacon(Beacon newBeacon)
    {
        if (SpeakBeacons)
        {
            ActiveBeacon = newBeacon;
            var index = beacons.IndexOf(newBeacon.gameObject);
            narratorAudioSource.clip = clips[index];
            narratorAudioSource.time = 0;
            narratorAudioSource.Play();
        }
    }
}
