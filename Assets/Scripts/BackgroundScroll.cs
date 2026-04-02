using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeed;
        meshRenderer.material.mainTextureOffset = new Vector2(offsetX, 0f);
    }
}
