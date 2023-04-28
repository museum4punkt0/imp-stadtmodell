using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{    
    public static Room Instance;
    public List<PolyImageTrack> orbPosers;
    public bool alsoAlignRotation;

     // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: dodati pasovanja samo na sekvencama prema delu sobe!
        Vector3 avgPos = Vector3.zero;
        Vector3 avgForward = Vector3.zero;
        Vector3 avgUp = Vector3.zero;
        int ct = 0;
        foreach (var item in orbPosers)
        {
            if (item.everFound && item.roomReady)
            {
                ct++;
                avgPos += item.avgPos;
                avgForward += item.avgFwd;
                avgUp += item.avgUp;
            }
        }
        if (ct > 0)
        {
            avgPos /= ct;
            avgForward /= ct;
            avgUp /= ct;
            transform.position = avgPos;
            if (alsoAlignRotation)
             transform.LookAt(avgPos + avgForward, avgUp);
        }
    }
}
