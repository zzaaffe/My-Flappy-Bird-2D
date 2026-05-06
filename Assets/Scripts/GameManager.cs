using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score;
    public int bestScore = 0;
    public Text uiScore;
    public Text uiOverScore;
    public Text BestScore;
    public int Score
    { 
        get { return score; }
        set 
        { 
            score = value; 
            this.uiScore.text = score.ToString();
            this.uiOverScore.text = score.ToString();
        }
    }

    public enum GAME_STATUS
    {
        Ready,
        InGame,
        GameOver
    }
    public GAME_STATUS status;
    public GameObject panelReady;
    public GameObject panelGame;
    public GameObject panelGameOver;
    private GAME_STATUS Status
    {
        get { return status; }
        set { status = value; UpdateUI(); }
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        panelReady.SetActive(true);
        Player.Instance.OnDeath += Player_OnDeath;
        Player.Instance.OnScore = OnPlayerScore;
    }

    void OnPlayerScore(int score)
    {
        this.Score += score;
    }

    void Update()
    {

    }

    private void Player_OnDeath()
    {
        GameToOver();
    }
    private void Player_RePlay()
    {
        GameOverToRePlay();
    }

    public void GameReadyToPlay()
    {
        this.Status = GAME_STATUS.InGame;
        PiplelineManager.Instance.StartRun();
        Player.Instance.ReStart();
        Player.Instance.Fly();
    }

    public void GameToOver()
    {
        this.Status = GAME_STATUS.GameOver;
        PiplelineManager.Instance.StopRun();
        Player.Instance.Idle();
        BestScore.text = bestScore.ToString();
        if (score > bestScore)
        {
            bestScore = score;
        }
    }
    public void GameOverToRePlay()
    {
        this.Status = GAME_STATUS.Ready;
        Score = 0;
    }

    public void UpdateUI()
    {
        panelReady.SetActive(this.Status == GAME_STATUS.Ready);
        panelGame.SetActive(this.Status == GAME_STATUS.InGame);
        panelGameOver.SetActive(this.Status == GAME_STATUS.GameOver);
    }
}
