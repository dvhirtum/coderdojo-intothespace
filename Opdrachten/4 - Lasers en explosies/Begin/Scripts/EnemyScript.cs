using UnityEngine;

public class EnemyScript : MonoBehaviour 
{
	public float Speed;
	
	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * Speed;
	}
}
