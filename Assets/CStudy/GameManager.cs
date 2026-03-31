using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Clear
}

public class GameManager : MonoBehaviour
{
    public EnemyController enemyCtrl;
    private GameState currentState = GameState.Playing;

    void Update()
    {
        Keyboard curKey = Keyboard.current;
        if (curKey != null && curKey.pKey.wasPressedThisFrame)
        {
            currentState = GameState.Paused;
        }
        if (curKey != null && curKey.oKey.wasPressedThisFrame)
        {
            currentState = GameState.GameOver;
        }
        if (curKey != null && curKey.zKey.wasPressedThisFrame)
        {
            currentState = GameState.Clear;
        }

        switch (currentState)
        {
            case GameState.Playing:
                Debug.Log("게임 진행 중");
                break;
            case GameState.Paused:
                Debug.Log("일시 정지");
                break;
            case GameState.GameOver:
                Debug.Log("게임 오버!");
                break;
            case GameState.Clear:
                Debug.Log("클리어!");
                break;
        }
    }
}