using UnityEngine;

public class PlayerLaserBoltScript : MonoBehaviour 
{
	public float Speed;

	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = transform.up * Speed;		
	}
}
