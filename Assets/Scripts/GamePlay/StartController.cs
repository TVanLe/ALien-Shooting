using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    [SerializeField] private Animator tileAnimator;
    [SerializeField] private TMP_Text highScoreInData;
    public void Archery1_ClickStart()
    {
        StartCoroutine(LoadScene(1 , 0.8f));
    }

    public void Archery1_ClickData()
    {
        highScoreInData.text = PlayerPrefs.GetInt("High1", 0).ToString();
    }
    
    public void Archery2_ClickStart()
    {
        StartCoroutine(LoadScene(2, 0.8f));
    }

    public void Archery2_ClickData()
    {
        highScoreInData.text = PlayerPrefs.GetInt("High2", 0).ToString();
    }

    IEnumerator LoadScene(int index, float timeDelay)
    {
        tileAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadSceneAsync(index);
    }
    public void OpenFacebook()
    {
        string urlToOpen = "https://www.facebook.com/cao.hitle";
        Application.OpenURL(urlToOpen);
    }
    
}
