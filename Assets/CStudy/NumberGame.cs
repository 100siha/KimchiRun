using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NumberGame : MonoBehaviour
{
    private int answer = 3;
    private const int MaxTry = 5;
    private int tryCount = 0;

    private bool isOver = false;

    void Start()
    {
        Debug.Log("=============================");
        Debug.Log("숫자 맞추기 게임 시작!");
        Debug.Log("1~5 사이의 숫자를 맞춰보세요!");
        Debug.Log("[1] [2] [3] [4] [5] 키를 누르세요.");
        Debug.Log("=============================");
    }

    void Update()
    {
        int input = -1;
        if (isOver) return;
        
        if(Input.GetKeyDown(KeyCode.Alpha1)) input = 1;
        if(Input.GetKeyDown(KeyCode.Alpha2)) input = 2;
        if(Input.GetKeyDown(KeyCode.Alpha3)) input = 3;
        if(Input.GetKeyDown(KeyCode.Alpha4)) input = 4;
        if(Input.GetKeyDown(KeyCode.Alpha5)) input = 5;

        if(input != -1)
        {
            CheckAnswer(input);
        }
    }

    private void CheckAnswer(int input)
    {
        tryCount++;
        Debug.Log($"입력: {input}, 시도 횟수: {tryCount}");

        if (input == answer)
        {
            Debug.Log($"😍 정답입니다!! {tryCount}번 만에 맞추셨습니다!");
            isOver = true;
            return; //정답 처리하고 끝내는 함수
        }

        if (tryCount >= MaxTry)
        {
            Debug.Log($"실패!! 정답은 {answer}였습니다.");
            isOver = true;
            return;
        }

        if (input > answer)
        {
            Debug.Log("👇 작은 숫자를 입력해 주세요!");
        }

        else
        {
            Debug.Log("👆 큰 숫자를 입력해 주세요!");
        }

    }
}
