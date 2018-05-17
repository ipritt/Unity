using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
	public float restartDelay = 5.0f;

	Animator anim;
	float restarttimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
			restarttimer += Time.deltaTime;

			if (restarttimer >= restartDelay) 
			{
				SceneManager.LoadScene ("Level 01");
			}
        }
    }
}
