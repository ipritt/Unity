using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    public Text restartText;
    public Text[] playerNamesText;
    public Text[] highScoresText;

    private int i;
    private int score = GameController.score;
    private string playerName = ConfirmButton.playerName;
    private string filePath;
    private bool fileExists = true;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/SpaceShooterScoreData.dat";
        restartText.text = "Press 'R' to restart!";
        Load();
        if(!fileExists)
        {
            Save();
        }
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void Save()
    {
        int x;
        FileStream file;
        HighScoreData data = new HighScoreData();
        BinaryFormatter bf = new BinaryFormatter();
        if (fileExists)
        {
            file = File.Open(filePath, FileMode.Open);
            for (i = 0; i < playerNamesText.Length; i++)
            {
                data.playerName[i] = playerNamesText[i].text;
                if(Int32.TryParse(highScoresText[i].ToString(), out x))
                {
                    data.highScore[i] = x;
                }
            }
        }
        else
        {
            file = File.Create(filePath);
            //  Initialize string array
            for (i = 0; i < data.playerName.Length; i++)
            {
                data.playerName[i] = "";
            }
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
                highScoresText[i].text = data.highScore[i].ToString();
            }
        }
        else
        {
            fileExists = false;
        }
    }
}

[Serializable]
class HighScoreData
{
    public string[] playerName = new string[10];
    public int[] highScore = new int[10];
}
