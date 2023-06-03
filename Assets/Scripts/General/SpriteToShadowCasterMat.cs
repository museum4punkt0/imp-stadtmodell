using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToShadowCasterMat : MonoBehaviour
{
    public MeshRenderer mr;
    public SpriteRenderer sr;
    public Material mat;
    private void Start()
    {
        mat = mr.material;
    }
    private void LateUpdate()
    {
        mat.mainTexture = sr.sprite.texture;
    }
}
