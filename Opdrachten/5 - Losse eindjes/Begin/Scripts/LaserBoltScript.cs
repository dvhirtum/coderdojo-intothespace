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
