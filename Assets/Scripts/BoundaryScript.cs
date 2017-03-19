using UnityEngine;

public class BoundaryScript : MonoBehaviour 
{
	void OnTriggerExit2D(Collider2D other)
	{
		Destroy(other.gameObject);
	}
}
