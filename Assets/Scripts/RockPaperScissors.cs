using UnityEngine;

public class RockPaperScissors : MonoBehaviour
{
    private int playerScore;
    private int cpuScore;
    private int round;
    private bool isOver;

    void Start()
    {
        playerScore = 0;
        cpuScore = 0;
        round = 1;

        Debug.Log("[1] 가위  [2] 바위  [3] 보");
    }

    void Update()
    {
        if (isOver) return;

        int playerChoice = 0;

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            playerChoice = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            playerChoice = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            playerChoice = 3;
        }

        if (playerChoice != 0)
        {
            int cpuChoice = Random.Range(1, 4);

            Debug.Log($"플레이어 선택: {GetChoiceName(playerChoice)} / 컴퓨터 선택: {GetChoiceName(cpuChoice)}");

            Judge(playerChoice, cpuChoice);
        }
    }

    string GetChoiceName(int choice)
    {
        switch (choice)
        {
            case 1: return "가위";
            case 2: return "바위";
            case 3: return "보";
            default: return "";
        }
    }

    void Judge(int playerChoice, int cpuChoice)
    {
        if (playerChoice == cpuChoice)
        {
            Debug.Log("무승부");
        }
        else if ((playerChoice == 1 && cpuChoice == 3) ||
                 (playerChoice == 2 && cpuChoice == 1) ||
                 (playerChoice == 3 && cpuChoice == 2))
        {
            Debug.Log("플레이어 승!");
            playerScore++;
        }
        else
        {
            Debug.Log("컴퓨터 승!");
            cpuScore++;
        }

        Debug.Log($"현재 점수: 플레이어 {playerScore} - 컴퓨터 {cpuScore}");

        round++;

        if (round > 3)
        {
            ShowFinalResult();
        }
        else
        {
            Debug.Log("[1] 가위  [2] 바위  [3] 보");
        }
    }

    void ShowFinalResult()
    {
        if (playerScore > cpuScore)
        {
            Debug.Log("최종 승리!");
        }
        else if (playerScore < cpuScore)
        {
            Debug.Log("최종 패배!");
        }
        else
        {
            Debug.Log("최종 무승부!");
        }

        isOver = true;
    }
}
