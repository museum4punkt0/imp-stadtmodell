using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDebugHelper : MonoBehaviour
{
    ARPlane m_Plane;
    public GameObject marker;
    // Start is called before the first frame update
    void Start()
    {
        m_Plane = GetComponent<ARPlane>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnBoundaryChanged(ARPlaneBoundaryChangedEventArgs eventArgs)
    {
        marker.transform.position = m_Plane.center;
    }

    void OnEnable()
    {
        m_Plane.boundaryChanged += OnBoundaryChanged;
        OnBoundaryChanged(default(ARPlaneBoundaryChangedEventArgs));
    }

    void OnDisable()
    {
        m_Plane.boundaryChanged -= OnBoundaryChanged;
    }
}
