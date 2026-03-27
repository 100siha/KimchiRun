using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    void Update()
    {
        // 1. 키보드 장치 체크 
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // 2. 해당 키 '누르는 순간 한번만' 실행
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("스페이스바를 눌렀습니다!!");
        }

        // 3. 키 '누르고 있는 동안 계--속' 실행
        if (keyboard.zKey.isPressed)
        {
            Debug.Log("Z 키를 누르는 중...");
        }
    }
}
