using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public Text scoreText;
	public Text gameOverText;
    public Text levelText;
    public Text shipDestroyedText;
	public GameObject[] hazards;
    public GameObject[] playerShips;
    public GameObject player;
	public Vector3 spawnValues;
	public int hazardCount;
    public int maxHazards;
    public static int level;
    public static int shipCount;
    public static int score;
	public float spawnWait;
    public float minSpawnWait;
	public float startWait;
	public float waveWait;

    private Vector3 playerSpawnPos;
    private Quaternion playerSpawnRot;
    private bool shipDestroyed;
	private bool gameOver;

    void Start()
	{
		gameOver = false;
        shipDestroyed = false;
		gameOverText.text = "";
        shipDestroyedText.text = "";
        levelText.text = level.ToString();
		score = 0;
        level = 0;
        shipCount = 2;
        playerSpawnPos = new Vector3(0.0f, 0.0f, 0.0f);
        playerSpawnRot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		UpdateScore();
        UpdateLevel();
        Instantiate(player, playerSpawnPos, playerSpawnRot);
		StartCoroutine(SpawnWaves());
	}

	void Update()
	{
		
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for(int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if(gameOver)
			{
                SceneManager.LoadScene("High Scores");
				break;
			}
            else
            {
                if (!shipDestroyed)
                {
                    UpdateLevel();
                }
                else
                {
                    shipDestroyedText.text = "";
                    Instantiate(player, playerSpawnPos, playerSpawnRot);
                    playerShips[shipCount].SetActive(false);
                    shipDestroyed = false;
                    yield return new WaitForSeconds(waveWait / 2);
                }
            }
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.text = "You are TOAST!!!";
		gameOver = true;
	}

    public void UpdateLevel()
    {
        level++;
        levelText.text = "Wave: " + level;
        if(spawnWait >= minSpawnWait && level != 1)
        {
            if (level < 30 && spawnWait >= 0.2f)
            {
                spawnWait -= .05f;
            }
            else if(level >= 30)
            {
                spawnWait -= .01f;
            }
        }
        if (hazardCount <= maxHazards && level != 1)
        {
            hazardCount++;
        }
    }

    public void ShipDestroyed()
    {
        shipCount--;
        shipDestroyedText.text = "The fragments of your ship are scattered and floating through space!!!";
        shipDestroyed = true;
    }
}
