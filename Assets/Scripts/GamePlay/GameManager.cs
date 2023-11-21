using System;
using System.Collections;
using System.Collections.Generic;
using MadsBangH.ArcheryGame;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private DataSystem dataSystem; 
    private bool isWaitingForFirstTargetShot;
    public  bool IsWaitingForFirstTargetShot  => isWaitingForFirstTargetShot;
    private bool startNewGameOnTap;
    private bool IsGameLost;
    private int CurrentScore;
    private int HighScore;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        dataSystem = GetComponent<DataSystem>();

    }
    
    
    private void Start()
    {
        GetHighSCoreFromJson();
        enemySpawner.LoadFirstEnemy();
        enemySpawner.gameObject.SetActive(false);
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
            enemySpawner.gameObject.SetActive(true);
        }

        if (IsGameLost)
        {
            enemySpawner.gameObject.SetActive(false);
        }
    }

    public void GetHighSCoreFromJson()
    {
        HighScore = dataSystem.LoadHighScoreFromJson(1);
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

        dataSystem.SaveToJson(1, HighScore);
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
}
