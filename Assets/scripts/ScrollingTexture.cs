using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {
	public Vector2 scrollSpeed = new Vector2(0.5f, 0.5f);
    public Color color = Color.white;

    private Renderer rendererComponent;

    void Start()
    {
        rendererComponent = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = Time.time * scrollSpeed;
        rendererComponent.material.SetTextureOffset("_MainTex", offset);
        rendererComponent.material.SetColor("_TintColor", color);
    }
}
