using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {

        // 배열
        /*
        string[] enemies = new string[3];
        enemies[0] = "슬라임";
        enemies[1] = "고블린";
        enemies[2] = "오크";
        */

        string[] enemies = {"슬라임", "고블린", "오크", "나쁜X"};

        for(int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(enemies[i]);
        }

    }

    void Update()
    {
        
    }

}
