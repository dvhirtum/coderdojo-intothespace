#Opdracht 3 - De tegenstanders

* Om te beginnen gaan we 4 Prefabs maken voor de verschillende tegenstanders:
    * Sleep "meteorBrown_big1" van Graphics/Rocks naar de Hierarchy (dit maakt een gameobject op positie 0,0,0, zodat we dat niet hoeven te resetten)
    * Hernoem het gameobject "Asteroid"
    * Voeg een RigidBody2D toe (Gravity Scale = 0)
    * Voeg een CircleCollider2D toe
        * Radius = 0.44
    * Maak een C# script "AsteroidScript" in de Scripts folder, met de volgende inhoud

```cs
using UnityEngine;

public class AsteroidScript : MonoBehaviour 
{
	public float MinTumble;
	public float MaxTumble;
	public float Speed;
	
	void Start () 
	{
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.angularVelocity = Random.Range(MinTumble, MaxTumble);
		rigidBody.velocity = -1 * transform.up * Speed;
	}
}
```
* Voeg het script toe aan het Asteroid gameobject
    * Min Tumble = 7
    * Max Tumble = 12
    * Speed = 2
* Sleep het Asteroid gameobject naar de Prefabs folder
* Deze Prefab gaan we nu hergebruiken om de andere tegenstanders te maken (let op: doe onderstaande stappen op het gameobject in de hierarchy, niet op de prefab):
    * EnemyBlue:
        * Verander de naam in "EnemyBlue"
        * Sprite = "enemyBlue3" uit de Graphics\Enemy folder
        * Circle collider Radius = 0.45
        * Maak een nieuw script "EnemyScript" in de Scripts folder met de volgende inhoud:

```cs
using UnityEngine;

public class EnemyScript : MonoBehaviour 
{
	public float Speed;
	
	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * Speed;
	}
}
```

* Verwijder het script "AsteroidScript" van het gameobject en voeg "EnemyScript" toe
    * Speed = 1
* Sleep "EnemyBlue" van de hierarchy naar de prefabs folder
* EnemyGreen:
    * Verander de naam in "EnemyGreen"
    * Sprite = "enemyGreen1" uit de Graphics\Enemy folder
    * Circle collider Radius = 0.41
    * Enemy script Speed = 2.5
    * Sleep naar prefabs folder
* EnemyRed:
    * Verander de naam in "EnemyRed"
    * Sprite = "enemyRed2" uit de Graphics\Enemy folder
    * Enemy script Speed = 3
    * Sleep naar prefabs folder
* Verwijder het gameobject van de hierarchy, dat is niet meer nodig

* Nu moeten we de tegenstanders gaan spawnen (op semi-willekeurige momenten laten verschijnen)
    * Voeg een leeg gameobject toe aan de hierarchy, noem het "GameController"
    * Maak een nieuw C# script in de Scripts folder met de naam "GameControllerScript"

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
```

* Voeg het nieuwe script toe aan de GameController
    * Size = 4
    * element 0
        * Enemy = Asteroid (de prefab)
        * Count = 4
        * Spawn Wait = 1
        * Start Wait = 1
        * Wave Wait = 5
    * element 1
        * Enemy = EnemyBlue
        * Count = 3
        * Spawn Wait = 1
        * Start Wait = 2
        * Wave Wait = 6
    * element 2
        * Enemy = EnemyGreen
        * Count = 3
        * Spawn Wait = 2
        * Start Wait = 8
        * Wave Wait = 8
    * element 3
        * Enemy = EnemyRed
        * Count = 2
        * Spawn Wait = 2
        * Start Wait = 14
        * Wave Wait = 10

* Als je nu de game uitprobeert dan verschijnen er als het goed is verschillende tegenstanders die langzaam (of snel) van boven naar beneden over het beeld vliegen
    * Je kan nog niet geraakt worden, dat gaan we in de volgende opdracht oplossen, maar eerst is er nog een ander probleem
    * Als je tijdens het spelen naar de hierarchy kijkt, dan zie je dat alle tegenstanders blijven bestaan
    * Uiteindelijk zou dit er toe lijden dat het geheugen van je computer vol komt te zitten, waarna het spel (of de computer) crasht
    * Om dit te voorkomen gaan we alle gameobject die de speler niet vernietigd heeft opruimen als ze uit het scherm vliegen:
        * Voeg een leeg gameobject toe met de naam "Boundary"
            * Scale: x=9, y=11, z=2
        * Voeg een BoxCollider2D toe aan de Boundary, IsTrigger aangevinkt
        * Maak een nieuw script "BoundaryScript" en voeg het toe aan de Boundary. Inhoud van het script:
```cs
using UnityEngine;

public class BoundaryScript : MonoBehaviour 
{
	void OnTriggerExit2D(Collider2D other)
	{
		Destroy(other.gameObject);
	}
}
```

* Nu zullen alle objecten die het scherm uit gaan opgeruimd worden