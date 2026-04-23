using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("이동 속도 배율")]
    public float speedMultiplier = 1f; // 1이면 기본속도, 0.8이면 약간 느리게, 1.2면 약간 빠르게

    void Update()
    {
        float moveSpeed = GameManager.Instance.CalculateGameSpeed();
        transform.position += Vector3.left * moveSpeed * speedMultiplier * Time.deltaTime;
    }
}