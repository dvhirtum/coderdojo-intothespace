using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float Speed;

	void FixedUpdate() {
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
}
