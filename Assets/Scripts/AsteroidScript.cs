using UnityEngine;

public class AsteroidScript : MonoBehaviour {
	public float MinTumble;
	public float MaxTumble;
	public float Speed;
	
	void Start () {
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.angularVelocity = Random.Range(MinTumble, MaxTumble);
		rigidBody.velocity = -1 * transform.up * Speed;
	}
}
