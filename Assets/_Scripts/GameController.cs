using UnityEngine;
using System.Collections;

enum state {GAMEPLAY,GAMEOVER,RESTART};

public class GameController : MonoBehaviour 
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	int score = 0;

	state currentState;

	void Start () 
	{
		currentState = state.GAMEPLAY;

		scoreText.text = "Score: " + score;
		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{
		if(currentState == state.RESTART)
			if(Input.GetKeyDown(KeyCode.R))
				Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			for (int i = 0; i < hazardCount; i++) 
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

				Instantiate (hazard, spawnPosition, Quaternion.identity);

				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if(currentState == state.GAMEOVER)
			{
				restartText.gameObject.SetActive(true);
				currentState = state.RESTART;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverText.gameObject.SetActive(true);
		currentState = state.GAMEOVER;
	}
}
