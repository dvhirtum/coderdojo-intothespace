using UnityEngine;

public class ShootScript : MonoBehaviour 
{
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
		}
	}
}
