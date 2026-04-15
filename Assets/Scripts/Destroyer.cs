using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float cameraLeftEdge = mainCamera.transform.position.x -
                mainCamera.orthographicSize * mainCamera.aspect;
        float objectRightEdge = spriteRenderer.bounds.max.x;

        if (objectRightEdge < cameraLeftEdge)
        {
            Destroy(gameObject);
        }
    }
}
