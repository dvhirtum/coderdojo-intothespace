#Opdracht 4 - Lasers en explosies

* Voor het schieten van lasers gaan we twee prefabs maken, PlayerLaserBolt en EnemyLaserBolt
    * Sleep "laserGreen1" van Graphics/Lasers naar de hierarchy
        * Naam = PlayerLaserBolt
        * Voeg een RigidBody2D toe (Gravity Scale = 0)
        * Voeg een BoxCollider2D toe (Size x=0.09, y=0.54)
        * Voeg een script toe (aan de scripts folder en koppel aan gameobject), "LaserBoltScript"
```cs
using UnityEngine;

public class LaserBoltScript : MonoBehaviour 
{
	public float Speed;
	public bool ShootUp;

	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = (ShootUp ? 1 : -1) * transform.up * Speed;		
	}
}
```
* Speed = 10
* Shoot up aangevinkt
* Sleep het gameobject naar de prefabs folder
* Pas de naam van het gameobject (in de hierarchy) aan naar "EnemyLaserBolt"
* Het enige dat hoeft te veranderen is Speed = 5 en shoot up uitgevinkt bij het script
* Sleep het gameobject opnieuw naar de prefabs folder
* verwijder het gameobject van de hierarchy

* De gameobjecten die kunnen schieten (Player, EnemyGreen en EnemyRed) hebben een punt nodig waar vandaan de laserbolt kan vertrekken
    * Voeg aan Player een leeg gameobject toe
        * Naam = ShotSpawn
        * Position: X=0, Y=0.6555326, z=0
    * Voeg aan EnemyGreen (prefab) een leeg gameobject toe
        * Naam = ShotSpawn
        * Position: X=0, Y=-0.438956, Z=0
    * Voeg aan EnemyRed (prefab) een leeg gameobject toe
        * Naam = ShotSpawn
        * Position: X=0, Y=-0.438956, Z=0

* Maak een nieuw script, "ShootScript"
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
		}
	}
}
```

* Voeg ShootScript aan Player toe
    * Fire Rate = 0.3
    * Laser Bolt = PlayerLaserBolt (prefab)
    * Shot Spawn = ShotSpawn (gameobject onder Player)
* Voeg ShootScript aan EnemyGreen (prefab) toe
    * Fire Rate = 2
    * Laser Bolt = EnemyLaserBolt (prefab)
    * Shot Spawn = ShotSpawn (gameobject onder EnemyGreen)
* Voeg ShootScript aan EnemyRed (prefab) toe
    * Fire Rate = 2
    * Laser Bolt = EnemyLaserBolt (prefab)
    * Shot Spawn = ShotSpawn (gameobject onder EnemyRed)

* Als je nu het spel speelt, dan zullen zowel jijzelf als de groene en rode tegenstanders schieten, maar iets raken? Ho maar.
* In eerdere stappen hebben we vaak colliders toegevoegd om te kunnen bepalen of objecten elkaar raken (denk aan de boundary)
* Voor de volgende stappen moeten we niet alleen weten dat we iets geraakt hebben, maar ook wat precies
* Daarvoor gebruiken we tags
    * Maak 3 tags, Enemy, PlayerLaser en EnemyLaser
    * Geef alle 4 de tegenstander-prefabs de tag Enemy
    * Geef de prefab PlayerLaserBolt de tag PlayerLaser
    * Geef de prefab EnemyLaserBolt de tag EnemyLaser

* Nu kunnen we collision detection gaan invoeren, pas PlayerScript als volgt aan:

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
			Destroy(gameObject);
		}
	}
}
```

* Voeg de prefab "explosion_player" uit Explosions/Prefabs toe aan het Player gameobject bij Player Script - Explosion
* Speel het spel en vlieg ergens tegen aan. KABOOM! Maar nu moeten we nog iets terug kunnen doen
* Als alle tegenstanders gelijk op de eerste hit ontploffen, dan zou het spel wel erg makkelijk zijn
* Dus we willen kunnen bijhouden hoe vaak een tegenstander geraakt kan worden
* En het zou wel mooi zijn als we kunnen zien als we iets raken
* Om dat laatste mogelijk te maken, gaan we weer een nieuwe prefab maken:
    * Sleep "laserGreen14" van Graphics/Lasers naar de hierarchy
    * Hernoem naar "LaserHit"
    * Sleep naar prefabs folder

* Om te zorgen dat deze LaserHit prefab heel even zichtbaar wordt als een tegenstander geraakt wordt en daarna weer verdwijnt gaan we een nieuw script maken: DestroyScript
```cs
using UnityEngine;

public class DestroyScript : MonoBehaviour 
{
	public float Delay;

	void Start () 
	{		
		Destroy(gameObject, Delay);
	}
}
```
* Voeg het script toe aan de prefab LaserHit en stel Delay in op 0.05, dus na 0.05 seconden zal het LaserHit object zichzelf verwijderen

* Nu hebben we nog een script nodig om bij te houden hoe vaak een tegenstander geraakt wordt, de LaserHit prefab te laten zien en natuurlijk een coole explosie te maken!
* Maak een nieuw script "LaserHitScript"
```cs
using UnityEngine;

public class LaserHitScript : MonoBehaviour 
{
	public int Health;
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
				
				Destroy(gameObject);
			}
		}		
	}
}
```
* Voeg dit script aan alle vier de tegenstander-prefabs toe
* Asteroid:
    * Health = 1
    * Laser Hit = LaserHit (prefab)
    * Explosion = explosion_asteroid (prefab uit Explosions/Prefabs)
* EnemyBlue:
    * Health = 2
    * Laser Hit = LaserHit (prefab)
    * Explosion = explosion_enemy (prefab uit Explosions/Prefabs)
* EnemyGreen:
    * Health = 2
    * Laser Hit = LaserHit (prefab)
    * Explosion = explosion_enemy (prefab uit Explosions/Prefabs)
* EnemyRed:
    * Health = 4
    * Laser Hit = LaserHit (prefab)
    * Explosion = explosion_enemy (prefab uit Explosions/Prefabs)

* Nu kun je terug schieten! Maar er is nog een klein probleem, als je naar de hierarchy kijkt terwijl je speelt zul je zien dat er steeds meer explosion objecten bestaan
* Dat komt omdat deze objecten niet bewegen, dus ook nooit de boundary raken. Maar hiervoor hebben we al een oplossing, namelijk het DestroyScript
* Voeg DestroyScript toe aan alledrie de explosion prefabs, met een delay van 2 seconden
* Nu worden oude explosies ook opgeruimd