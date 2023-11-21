using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private const string SCORE_BUMED = "Score Bumped";
    private const string VISIBLE = "Visible";
    
    [SerializeField] private TMP_Text scoreLabel = default;
    [SerializeField] private Animator scoreLabelAnimator = default;
    [SerializeField] private Animator startGameLabelAnimator = default;
    [SerializeField] private Animator gameOverLabelsAnimator = default;
    [SerializeField] private TMP_Text gameOverScoreLabel = default;
    [SerializeField] private Button restarrBTN;
    public static bool IsNewGame;
    public static bool IsGameOver;

    private int IndexScene => SceneManager.GetActiveScene().buildIndex; 
    
    private void Start()
    {
        startGameLabelAnimator.SetBool(VISIBLE, true);
        IsNewGame = false;
        IsGameOver = false;
    }

    private void Update()
    {
        if (IndexScene == 1)
        {
            UpdateScore1();
        }
        else if (IndexScene == 2)
        {
            UpdateScore2();
        }
        
        
        if (IsNewGame)
        {
            startGameLabelAnimator.SetBool(VISIBLE, false);
            scoreLabelAnimator.SetBool(VISIBLE, true);
            IsNewGame = false;
            IsGameOver = false;
        }

        if (IsGameOver)
        {
            scoreLabelAnimator.SetBool(VISIBLE, false);
            gameOverLabelsAnimator.SetBool(VISIBLE,true);
            restarrBTN.gameObject.SetActive(true);
            if (IndexScene == 1)
            {
                NotiFyGameOver1();
            }
            else if (IndexScene == 2)
            {
                NotiFyGameOver2();
            }
            IsGameOver = false;
        }
    }

    private void UpdateScore1()
    {
        if (scoreLabel.text != GameManager.Instance.GetCurScore().ToString())
        {
            scoreLabelAnimator.SetTrigger(SCORE_BUMED);    
        }
        
        scoreLabel.text = GameManager.Instance.GetCurScore().ToString();
    }
    private void UpdateScore2()
    {
        if (scoreLabel.text != Archery2Manager.Instance.GetCurScore().ToString())
        {
            scoreLabelAnimator.SetTrigger(SCORE_BUMED);    
        }
        
        scoreLabel.text = Archery2Manager.Instance.GetCurScore().ToString();
    }
    

    private void NotiFyGameOver1()
    {
        string textTemplate = gameOverScoreLabel.text;
        gameOverScoreLabel.text = textTemplate
            .Replace("{score}", GameManager.Instance.GetCurScore().ToString())
            .Replace("{highscore}", GameManager.Instance.GetHighScore().ToString());;
    }
    
    private void NotiFyGameOver2()
    {
        string textTemplate = gameOverScoreLabel.text;
        gameOverScoreLabel.text = textTemplate
            .Replace("{score}", Archery2Manager.Instance.GetCurScore().ToString())
            .Replace("{highscore}", Archery2Manager.Instance.GetHighScore().ToString());;
    }

    public void Click_Home()
    {
        StartCoroutine(LoadSceneIndex(0,0.4f));
    }

    public void Click_Restart()
    {
        restarrBTN.gameObject.SetActive(false);
        gameOverLabelsAnimator.SetBool(VISIBLE,false);
        StartCoroutine(LoadSceneIndex(SceneManager.GetActiveScene().buildIndex, 1.3f));
    }

    IEnumerator LoadSceneIndex(int index , float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadSceneAsync(index);
    }
}
