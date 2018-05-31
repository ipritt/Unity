using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    public Text restartText;
    public Text reEnterNameText;
    public Text[] playerNamesText;
    public Text[] highScoresText;

    private int i;
    private int score = GameController.score;
    private int[] highScores = new int[10];
    private string playerName = ConfirmButton.playerName;
    private string filePath;
    private List<string> nameList = new List<string>(10);
    private List<int> scoreList = new List<int>(10);

    void Awake()
    {
        filePath = Application.persistentDataPath + "/SpaceShooterScoreData.dat";
        restartText.text = "Press 'R' to restart!";
        reEnterNameText.text = "Press 'N' to Re-Enter Name!";
    }

    void Start()
    {
        CreateFile();
        Load();
        SetScore();
        Save();
        Load();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Intro");
        }
    }

    public void Save()
    {
        HighScoreData data = new HighScoreData();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);
        for (i = 0; i < data.playerName.Length; i++)
        {
            data.playerName[i] = nameList[i];
            data.highScore[i] = scoreList[i];
        }

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            HighScoreData data = (HighScoreData)bf.Deserialize(file);
            file.Close();

            for(i = 0; i < playerNamesText.Length; i++)
            {
                playerNamesText[i].text = data.playerName[i];
                highScores[i] = data.highScore[i];
            }
            for(i = 0; i < highScores.Length; i++)
            {
                if(highScores[i] != 0)
                {
                    highScoresText[i].text = highScores[i].ToString(); 
                }
            }
        }
    }

    private void CreateFile()
    {
        if (!File.Exists(filePath))
        {
            HighScoreData data = new HighScoreData();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(filePath);

            //  Initialize string array
            for (i = 0; i < 10; i++)
            {
                data.playerName[i] = "";
            }

            bf.Serialize(file, data);
            file.Close();
        }
    }

    private void SetScore()
    {
        for(i = 0; i < playerNamesText.Length; i++)
        {
            nameList.Insert(i, playerNamesText[i].text);
        }
        for(i = 0; i < highScores.Length; i++)
        {
            scoreList.Insert(i, highScores[i]);
        }

        for(i = 0; i < scoreList.Count; i++)
        {
            if(score > scoreList[i])
            {
                scoreList.Insert(i, score);
                nameList.Insert(i, playerName);
                scoreList.RemoveAt(10);
                nameList.RemoveAt(10);
                break;
            }
        }
    }
}

[Serializable]
class HighScoreData
{
    public string[] playerName = new string[10];
    public int[] highScore = new int[10];
}
