using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBumpMap : MonoBehaviour
{
    private Renderer renderer;
    
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    
    private void FixedUpdate()
    {
        var offset = renderer.material.GetTextureOffset("_BumpMap");
        renderer.material.SetTextureOffset("_BumpMap", new Vector2(offset.x + 0.05f, offset.y + 0.05f));
    }
}
