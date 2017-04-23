using UnityEngine;

public class DestroyScript : MonoBehaviour 
{
	public float Delay;

	void Start () 
	{		
		Destroy(gameObject, Delay);
	}
}
