using System;
using System.Collections;
using System.Collections.Generic;
using MadsBangH.ArcheryGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Archery2Manager : MonoBehaviour
{
    private EnemyFlySpawner flySpawner;
    private DataSystem dataSystem; 
    private bool isWaitingForFirstTargetShot;
    public  bool IsWaitingForFirstTargetShot  => isWaitingForFirstTargetShot;
    private bool startNewGameOnTap;
    private bool IsGameLost;
    private int CurrentScore;
    private int HighScore;

    public static Archery2Manager Instance;

    private void Awake()
    {
        Instance = this;
        flySpawner = FindObjectOfType<EnemyFlySpawner>();
        dataSystem = GetComponent<DataSystem>();

    }
    
    
    private void Start()
    {
        GetHighSCoreFromJson();
        flySpawner.LoadFirstEnemy();
        flySpawner.gameObject.SetActive(false);
        startNewGameOnTap = true;
        isWaitingForFirstTargetShot = true;
        IsGameLost = false;
    }

    private void Update()
    {
        if (!isWaitingForFirstTargetShot && startNewGameOnTap)
        {
            AudioManger.Instance.NewGame();
            StartCoroutine(WaitingForNewGame(0.4f));
            startNewGameOnTap = false;
            flySpawner.gameObject.SetActive(true);
        }

        if (IsGameLost)
        {
            flySpawner.gameObject.SetActive(false);
        }
    }

    public void GetHighSCoreFromJson()
    {
        HighScore = dataSystem.LoadHighScoreFromJson(2);
    }
    public void EnemyWasHit()
    {
        CurrentScore++;
        isWaitingForFirstTargetShot = false;
    }
    
    
    public void GameLost()
    {
        if (CurrentScore > HighScore)
            HighScore = CurrentScore;

        dataSystem.SaveToJson(2, HighScore);
        IsGameLost = true;
        StartCoroutine(WaitingForShowGameOver(1f));
    }
    
    private IEnumerator WaitingForShowGameOver(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        CanvasController.IsGameOver = true;
    }

    private IEnumerator WaitingForNewGame(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        CanvasController.IsNewGame = true;
    }
    
    public int GetCurScore() => CurrentScore;

    public int GetHighScore() => HighScore;

    public void LoadScene(float timeDelay)
    {
        StartCoroutine(Load(timeDelay));
    }
    
    IEnumerator Load(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
