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
	public Wave[] Waves;

	void Start () 
	{
		foreach (Wave wave in Waves) 
		{
			StartCoroutine(spawnWaves(wave));
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
