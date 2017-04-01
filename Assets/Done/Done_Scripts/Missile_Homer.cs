using UnityEngine;
using System.Collections;

public class Missile_Homer : MonoBehaviour {
	public float speed;
	public int turnSpeed;
	public float visionAngle;
	
	private Done_GameController gameController;
	private Transform target;
	
	private const byte targetCheckWait = 120;
	private byte checkTime;
	
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
			
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		
		target = FindTarget ();
		rigidbody.velocity = transform.forward * speed;
		checkTime = 0;
	}
	
	void Update ()
	{
		//Attempt to find a target once every 'targetCheckWait' cycles or until a target is acquired.
		if (target == null && checkTime == targetCheckWait) 
		{
			target = FindTarget ();
			checkTime = 0;
		} else if (target != null) 
		{
			//Change orientation and velocity towards the target
			Vector3 lookPos = target.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
			rigidbody.velocity = transform.forward * speed;
		}
		else checkTime++;
		
	}
	
	/*
	 * Get the nearest target within a cone of vision. 
	 * If there are no hazards within the COV, return null.
	 */
	Transform FindTarget()
	{
		GameObject item = GameObject.FindGameObjectWithTag("Player");
		if (item == null)
			return null;
		return item.transform;
		
	}
}
