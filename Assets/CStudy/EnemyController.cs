using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameManager gameMgrObject;

    void Start()
    {
        TakeDamage(10);
        Debug.Log("state : ");
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("damage " + damage);
        return;
    }
} 
