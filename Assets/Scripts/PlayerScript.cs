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
