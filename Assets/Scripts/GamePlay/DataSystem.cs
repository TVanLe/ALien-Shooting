using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSystem : MonoBehaviour
{
    public void SaveToJson(int Mode , int score)
    {
        if (Mode == 1)
        {
            PlayerPrefs.SetInt("High1" , score);
        }

        if (Mode == 2)
        {
            PlayerPrefs.SetInt("High2" , score); 
        }
    }

    public int  LoadHighScoreFromJson(int Mode)
    {
        int data = 0;
        if (Mode == 1)
        {
            data = PlayerPrefs.GetInt("High1" , 0);
        }

        if (Mode == 2)
        {
            data = PlayerPrefs.GetInt("High2" , 0); 
        }

        return data;
    }
}
