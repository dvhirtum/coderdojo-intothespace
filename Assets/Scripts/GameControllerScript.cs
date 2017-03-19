using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemyType 
{
	public GameObject Enemy;
	public int Count;
	public float SpawnWait;
	public float StartWait;
	public float WaveWait;
}

public class GameControllerScript : MonoBehaviour 
{
	public EnemyType[] Enemies;

	void Start () 
	{
		foreach (EnemyType enemyType in Enemies) 
		{
			StartCoroutine(spawnWaves(enemyType));
		}
	}

	void Update () 
	{
		if (Input.GetKey("r"))
		{
			SceneManager.LoadScene("Scene_01", LoadSceneMode.Single);
		}
	}

	IEnumerator spawnWaves(EnemyType enemyType)
	{
		yield return new WaitForSeconds (enemyType.StartWait);

		while (true)
		{
			for (int i = 0; i < enemyType.Count; i++)
			{
				Vector2 spawnPosition = new Vector2 (Random.Range (-3, 3), 6);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (enemyType.Enemy, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (enemyType.SpawnWait);
			}
			yield return new WaitForSeconds (enemyType.WaveWait);
		}
	}
}
