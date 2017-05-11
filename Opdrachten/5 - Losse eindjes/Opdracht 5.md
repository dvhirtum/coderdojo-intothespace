#Opdracht 5 - Losse eindjes

* laser geluid 
    * audio source op Player
        * weapon_player uit Audio folder
        * play on awake uit
        * volume 0.4
    * audio source op EnemyGreen en EnemyRed
        * weapon_enemy uit Audio folder
        * play on awake uit
        * volume 0.5
    * aanpassen ShootScript:
```cs
using UnityEngine;

public class ShootScript : MonoBehaviour 
{
	public float FireRate = 0.5f;
	public GameObject LaserBolt;
	public Transform ShotSpawn;

	private float nextFire = 0.0f;

	void Update()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + FireRate;
			Instantiate(LaserBolt, ShotSpawn.position, ShotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}
	}
}
```

* achtergrond geluid
    * audio source op GameController
        * music_background uit Audio folder
        * play on awake aan
        * loop aan
        * volume 0.6

* score en text
* Voeg vier gameobjecten toe aan Main Camera
    * ScoreText
        * Position: X=0, Y=1, Z=10
        * GUI Text component
            * pixel offset x=5, y=-5
            * font: ARCADE (uit Fonts folder)
            * fontsize: 25
    * GameOverText
        * Position: X=0.5, Y=0.65, Z=10
        * GUI Text component
            * anchor: middle center
            * alignment: center
            * pixel offset x=5, y=-5
            * font: ARCADE (uit Fonts folder)
            * fontsize: 50
    * FinalScoreText
        * Position: X=0.5, Y=0.59, Z=10
        * GUI Text component
            * anchor: middle center
            * alignment: center
            * pixel offset x=5, y=-5
            * font: ARCADE (uit Fonts folder)
            * fontsize: 50
    * ReplayText
        * Position: X=0.5, Y=0.54, Z=10
        * GUI Text component
            * anchor: middle center
            * alignment: center
            * pixel offset x=5, y=-5
            * font: ARCADE (uit Fonts folder)
            * fontsize: 25
* Pas GameControllerScript aan (voegt ook herstart optie toe):
```cs
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
```
* Sleep de vier text objecten die je onder de main camera hebt gemaakt naar de GameController properties met dezelfde naam

* Geef GameController de tag "GameController"
* Pas LaserHitScript aan:
```cs
using UnityEngine;

public class LaserHitScript : MonoBehaviour 
{
	public int Health;
	public int ScoreValue;
	public GameObject LaserHit;
	public GameObject Explosion;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerLaser")
		{
			Instantiate (LaserHit, transform.position , transform.rotation);
			Destroy(other.gameObject);
			
			if (Health > 0)
				Health--;

			if (Health <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation);

				GameObject gameController = GameObject.FindWithTag("GameController");
				GameControllerScript script = gameController.GetComponent<GameControllerScript>();
				script.IncrementScore(ScoreValue);
				
				Destroy(gameObject);
			}
		}		
	}
}
```
* Vul de nieuwe score value property in bij alle vier de tegenstander-prefabs:
    * Asteroid: 10
    * EnemyBlue: 15 
    * EnemyGreen: 25
    * EnemyRed: 40
* Pas PlayerScript aan:
```cs
using UnityEngine;

public class PlayerScript : MonoBehaviour 
{
	public float Speed;
	public GameObject Explosion;

	void FixedUpdate() 
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.velocity = movement * Speed;

		rigidBody.position = new Vector2
		(
			Mathf.Clamp(rigidBody.position.x, -3, 3),
			Mathf.Clamp(rigidBody.position.y, -4.5f, 2)
		);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy" || other.tag == "EnemyLaser") 
		{
			Instantiate (Explosion, transform.position , transform.rotation);

			GameObject gameController = GameObject.FindWithTag("GameController");
			GameControllerScript script = gameController.GetComponent<GameControllerScript>();
			script.GameOver();
			
			Destroy(gameObject);
		}
	}
}
```

* Speel het spel
    * Nu hoor je muziek, maken de lasers geluid bij het schieten, wordt er score bijgehouden en getoond en kun je herstarten als je af bent