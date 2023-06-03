using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTargetObjectSpawner : MonoBehaviour
{
    public List<ARSpawnImageObjectPair> pairs;
    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    public ARTrackedImageManager trackedImageManager;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var pair in pairs)
        {
            spawnedObjects.Add(pair.Image.name, pair.gameObject);
            Debug.Log("ADDED FOR " + pair.Image.name);
            pair.gameObject.SetActive(false);
        }    
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImagesChanged;  
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImagesChanged;

    }


    public void ImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var foundImage in args.added)
        {
            var gObject = spawnedObjects[foundImage.referenceImage.name];
            gObject.transform.parent = foundImage.transform;
            gObject.transform.localPosition = Vector3.zero;
            gObject.transform.localRotation = Quaternion.identity;
            gObject.SetActive(true);
        }
        foreach (var updatedImage in args.updated)
        {
            var gObject = spawnedObjects[updatedImage.referenceImage.name];
            gObject.transform.parent = updatedImage.transform;
            gObject.transform.localPosition = Vector3.zero;
            gObject.transform.localRotation = Quaternion.identity;
            gObject.SetActive(true);
        }
        foreach (var updatedImage in args.removed)
        {
            Debug.Log("REMOVED" + updatedImage.name);
            var gObject = spawnedObjects[updatedImage.referenceImage.name];
            gObject.transform.parent = null;
        }
    }

    public 

    // Update is called once per frame
    void Update()
    {
    }
}

[Serializable]
public class ARSpawnImageObjectPair
{
    public Texture2D Image;
    public GameObject gameObject;
}
