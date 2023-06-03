using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Manages the plane material color for each recognized plane based on
    /// the <see cref="UnityEngine.XR.ARSubsystems.TrackingState"/> enumeration defined in ARSubsystems.
    /// </summary>
    [RequireComponent(typeof(ARPlane))]
    [RequireComponent(typeof(MeshRenderer))]
    public class PlaneTrackingModeVisualizer : MonoBehaviour
    {
        public static List<ARPlane> allPlanes = new List<ARPlane>();
        public static bool mioFound = true;
        ARPlane m_ARPlane;
        MeshRenderer m_PlaneMeshRenderer;
        Color m_OriginalColor;
        GameObject pfalh;
        GameObject mio;
        void Awake()
        {
            m_ARPlane = GetComponent<ARPlane>();
            m_PlaneMeshRenderer = GetComponent<MeshRenderer>();
            m_OriginalColor =  m_PlaneMeshRenderer.material.color;
        }

        private void OnEnable()
        {
            if (!allPlanes.Contains(m_ARPlane))
            {
                allPlanes.Add(m_ARPlane);
            }
        }

        private void OnDisable()
        {
            allPlanes.Remove(m_ARPlane);
        }

        private void Start()
        {
            mioFound = true;
        }
        void Update()
        {
            if (pfalh == null)  pfalh = GameObject.Find("PfalhRoot"); 
            if (mio == null) mio = GameObject.Find("Mio");
            UpdatePlaneColor();

        }

        
        void UpdatePlaneColor()
        {

            Color planeMatColor = Color.blue;

            switch (m_ARPlane.trackingState)
            {
                case TrackingState.None:
                    planeMatColor = Color.grey;
                    break;
                case TrackingState.Limited:
                    planeMatColor = Color.red;
                    break;
                case TrackingState.Tracking:
                    planeMatColor = m_OriginalColor;
                    break;
            }
   
            planeMatColor.a = m_OriginalColor.a;

            if (MasterManager.Instance.worldMapReady)
            {
                if (PlayableManager.Instance.sceneState == PlayableManager.SceneState.Pfalh && pfalh != null)
                {
                    var faldist = Vector3.Distance(m_ARPlane.center, pfalh.transform.position);

                    if (faldist < 0.5f)
                    {
                        planeMatColor = Color.magenta;
                        pfalh.transform.position = m_ARPlane.center;
                    }
                }

                if (mioFound && (PlayableManager.Instance.sceneState == PlayableManager.SceneState.SessionStarted || PlayableManager.Instance.sceneState == PlayableManager.SceneState.LinaIntro) && mio != null)
                {
                    var miodist = Vector3.Distance(m_ARPlane.center, mio.transform.position);
                        if (miodist < 1 ) 
                    {
                        planeMatColor = Color.magenta;
                        Room.Instance.transform.forward = -m_ARPlane.normal;
                            Room.Instance.backupSimplerSystem = false;
                    }
                }
            }
            if (DebugManager.Instance.experimentalFeaturesOn)
            { 
            if (m_ARPlane.trackingState ==  TrackingState.Tracking && Vector3.Distance(m_ARPlane.center, PolyImageTrack.GiessenGrad.passerXFinder.position)<0.5f)
                {
                PolyImageTrack.GiessenGrad.passerX = m_ARPlane;
                planeMatColor = Color.red;
            }

            if (m_ARPlane.trackingState == TrackingState.Tracking && Vector3.Distance(m_ARPlane.center, PolyImageTrack.GiessenGrad.passerZFinder.position) < 0.5f)
            {
                PolyImageTrack.GiessenGrad.passerZ= m_ARPlane;
                planeMatColor = Color.blue;
            }
            }

            m_PlaneMeshRenderer.material.color = planeMatColor;

        }
    }
}
