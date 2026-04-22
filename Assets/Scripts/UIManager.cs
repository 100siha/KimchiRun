using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject IntroUI;
    public GameObject ItemSpawner;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 인스펙터에서 넣지 않으셨을 경우를 대비해 스크립트가 직접 이름을 기반으로 찾아서 연결합니다.
        if (IntroUI == null) IntroUI = GameObject.Find("IntroUI");
        if (ItemSpawner == null) ItemSpawner = GameObject.Find("ItemSpawner");
    }

    void Start()
    {
        IntroUI.SetActive(true);
        ItemSpawner.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            scoreText.text = "Score : " + GameManager.Instance.CalculateScore();
            highScoreText.text = "High Score : " + GameManager.Instance.highScore;
        }
        else
        {
            scoreText.text = "";
            highScoreText.text = "";
        }
    }
}
