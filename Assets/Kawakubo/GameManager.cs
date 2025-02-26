using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] DowCounter DowCounter;
    private static float Timer = 0;
    private static float Distance = 0;
    public static GameManager Instance;

    [SerializeField]
    private Obstacle _monday = default;

    public Obstacle Monday => _monday;

    private void Awake()
    {
        Instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            AudioManager.Instance.PlayBGM(BGMType.InGame);
            Timer = 0;
            Distance = 0;

            ScoreManager.Instance.ResetScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            Timer += Time.deltaTime;
        }
    }

    //逃げた距離の追加
    public void AddDistance(float distance)
    {
        Distance += distance;
    }

    //逃げた距離の参照
    public float GetDistance()
    {
        return Distance;
    }

    //時間の参照
    public float GetTime()
    {
        return Timer;
    }

    //ゲームオーバー時の処理
    public void GameOver()
    {
        ScoreManager.Instance.SetScore(Timer, DowCounter.GetResultWeek());
    }
}
