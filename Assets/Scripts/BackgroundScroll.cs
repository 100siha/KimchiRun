using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    // public float scrollSpeed = 0.5f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float scrollSpeed = GameManager.Instance.CalculateGameSpeed() / 10;
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
