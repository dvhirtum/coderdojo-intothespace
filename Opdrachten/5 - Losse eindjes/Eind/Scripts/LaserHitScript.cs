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
