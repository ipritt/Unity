using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    private int i;
    private int highscore;
    private string[] keyList = new string[10];
    private string[] playerArray = new string[10];
    public Text[] playerText = new Text[10];
    public Text[] scoreText = new Text[10];
    
	void Start ()
    {
        //  Set PlayerPrefs
		for(i = 0; i < keyList.Length; i++)
        {
            keyList[i] = "HighScore" + i.ToString();
        }
        for(i = 0; i < keyList.Length; i++)
        {
            if(PlayerPrefs.HasKey(keyList[i]))
            {
                highscore = PlayerPrefs.GetInt(keyList[i], highscore);
                if(highscore < GameController.score)
                {
                    Debug.Log(highscore);
                    SetScore();
                    break;
                }
            }
            else
            {
                SetScore();
                break;
            }
        }
        //  Load Data to Screen
        for(i = 0; i < playerArray.Length; i++)
        {
            if(PlayerPrefs.HasKey(keyList[i]))
            {
                playerText[i].text = playerArray[i];
                scoreText[i].text = PlayerPrefs.GetInt(keyList[i], highscore).ToString();
            }
        }
	}
    
    private void SetScore()
    {
        PlayerPrefs.SetInt(keyList[i], GameController.score);
        playerArray[i] = ConfirmButton.playerName;
    }
	
	void Update () {
		
	}
}
