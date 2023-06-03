using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeaconsData
{
    [SerializeField]
    public List<BeaconPosition> beacons;

    [SerializeField]
    public List<ARPersistentObjectData> persistentObjects;

    [SerializeField]
    public ARPersistentObjectData gisengrad;

}
