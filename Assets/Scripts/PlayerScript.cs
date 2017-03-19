using UnityEngine;

public class PlayerScript : MonoBehaviour 
{
	public float Speed;
	public GameObject Explosion;

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
		if (other.tag == "Enemy") 
		{
			Instantiate (Explosion, transform.position , transform.rotation);
			Destroy(gameObject);
		}
	}
}
