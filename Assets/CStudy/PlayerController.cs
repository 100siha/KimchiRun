using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool isJumping = false;

    void Update()
    {
        var key = Keyboard.current;
        if (key == null) return;

        // 방향 점프
        if (key.leftArrowKey.wasPressedThisFrame ||
            key.rightArrowKey.wasPressedThisFrame ||
            key.upArrowKey.wasPressedThisFrame)
        {
            if (isJumping)
            {
                Debug.Log("공중에서는 점프할 수 없습니다!");
            }
            else
            {
                isJumping = true;

                if (key.leftArrowKey.wasPressedThisFrame)
                    Debug.Log("왼쪽으로 점프!");
                else if (key.rightArrowKey.wasPressedThisFrame)
                    Debug.Log("오른쪽으로 점프!");
                else if (key.upArrowKey.wasPressedThisFrame)
                    Debug.Log("위로 점프!");
            }
        }

        // 착지
        if (key.spaceKey.wasPressedThisFrame)
        {
            isJumping = false;
            Debug.Log("착지!");
        }
    }
}