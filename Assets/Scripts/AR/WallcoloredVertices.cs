   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class WallcoloredVertices : TrackableStateResponder
{
    public List<Transform> pickPoints;
    public float elapsedTimeSinceLastFrame;
    public GetCameraFeed gcf;
    public MeshFilter mr;
    public MeshRenderer mrender;
    public bool haveColor;
    public Mesh mesh;
    public RawImage raw;
    public bool waitingForFrame;

    public void RawTextToggle()
    {
        raw.gameObject.SetActive(!raw.gameObject.activeSelf);
    }

    // Start is called before the first frame update
    void Start()
    {
        mrender.enabled = false;
        mesh = mr.mesh;
    }

    // Update is called once per frame
    void Update()
    {
        //var colors = new Color[4];
        //for (int i = 0; i < 4; i++)
        //{
        //    colors[i] = Random.ColorHSV(0,1,0,1,0,1);
        //}
        //mesh.colors = colors;

        elapsedTimeSinceLastFrame += Time.deltaTime;
        if (elapsedTimeSinceLastFrame > 0.06 && !waitingForFrame)
        {
            waitingForFrame = true;
            gcf.GetImageAsync(GetColorFromTexture);
            elapsedTimeSinceLastFrame = 0;
        }
    }

    void GetColorFromTexture(Texture2D texture)
    {
        waitingForFrame = false;
        if (texture == null)
        {
            raw.color = new Color(0, 1, 1, 0.8f);
        }
        else
        {
            raw.texture = texture;
            var colors = new Color[4];
            for (int i = 0; i < 4; i++)
            {
                var screenCoord = Vector3.one - Camera.main.WorldToViewportPoint(pickPoints[i].position);
                var avgColor = Color.black;
                var clrCt = 0;
                int pixelX;
                int pixelY;
                for (int a = -20; a <= 20; a+=5)
                {
                    for (int b = -20; b <= 20; b+=5)
                    {
                        pixelX = (int)Mathf.Lerp(0, texture.width, screenCoord.x ) + a;
                        pixelY = (int)Mathf.Lerp(0, texture.height, (screenCoord.y  + 0.04f) / 1.08f) + b;
                        if (pixelX>=0 && pixelX < texture.width && pixelY>=0 && pixelY < texture.height)
                        {
                            avgColor += texture.GetPixel(pixelX, pixelY);
                            clrCt++;
                        }
                    }
                }
                avgColor /= clrCt;
               //Debug.Log(avgColor);
                colors[i] = avgColor;
            }
            mesh.colors = colors;
            mesh.UploadMeshData(false);
            mrender.enabled = true;
        }

    }

    public override void StateChanged(TrackingState state)
    {

        waitingForFrame = false;
    }
}
