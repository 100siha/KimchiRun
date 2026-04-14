using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("속도")]
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
