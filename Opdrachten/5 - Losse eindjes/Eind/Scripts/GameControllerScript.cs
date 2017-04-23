using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave 
{
	public GameObject Enemy;
	public int Count;
	public float SpawnWait;
	public float StartWait;
	public float WaveWait;
}

public class GameControllerScript : MonoBehaviour 
{
	public GUIText ScoreText;
	public GUIText GameOverText;
	public GUIText FinalScoreText;
	public GUIText ReplayText;

	public Wave[] Waves;

	private int score;
	private bool gameOver;

	public void IncrementScore(int value)
	{
		score += value;
	}

	public void GameOver(){
		gameOver = true;
	}

	void Start () 
	{
		foreach (Wave wave in Waves) 
		{
			StartCoroutine(spawnWaves(wave));
		}
	}

	void Update () 
	{
		if (Input.GetKey("r"))
		{
			SceneManager.LoadScene("Scene_01", LoadSceneMode.Single);
		}
	}

	void FixedUpdate()
	{
		ScoreText.text = "SCORE: " + score;

		if (gameOver == true)
		{
			GameOverText.text = "GAME OVER";
			FinalScoreText.text = "" + score;
			ReplayText.text = "PRESS R TO REPLAY";
		}
	}

	IEnumerator spawnWaves(Wave wave)
	{
		yield return new WaitForSeconds (wave.StartWait);

		while (true)
		{
			for (int i = 0; i < wave.Count; i++)
			{
				Vector2 spawnPosition = new Vector2 (Random.Range (-3, 3), 6);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (wave.Enemy, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (wave.SpawnWait);
			}
			yield return new WaitForSeconds (wave.WaveWait);
		}
	}
}
