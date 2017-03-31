using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
	public int turnSpeed;
	public float moveSpeed;

	private Transform target;

	private const byte targetCheckWait = 120;
	private byte checkTime;
	private bool firing;
	
	void Start ()
	{

		GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerObject != null) {
				target = playerObject.transform;

				//Change orientation and velocity towards the target
				Vector3 lookPos = target.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation (lookPos);
				transform.rotation = rotation;

				InvokeRepeating ("Fire", delay, fireRate);
				firing = true;
		} else {
			//If player is currently destroyed
			firing = false;
			transform.Rotate(0f,180f,0f);
		}
		rigidbody.velocity = moveSpeed * Vector3.back;
	}

	void Update ()
	{
		//If no target has been acquired
		if (!firing) 
		{
			GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
			if (playerObject != null) 
			{
				target = playerObject.transform;
				InvokeRepeating ("Fire", delay, fireRate);
				firing = true;
			}
		} else 
		{
			//Check to make sure player has not been destroyed.
			if (target != null) 
			{
				//Change orientation towards the target
				Vector3 lookPos = target.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(lookPos);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
			} else 
			{
				//Stop firing because the player has been destroyed. Start searching for respawned player.
				CancelInvoke("Fire");
				firing = false;
			}
		}

	}
	
	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		audio.Play();
	}
}
