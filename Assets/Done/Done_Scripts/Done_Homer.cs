using UnityEngine;
using System.Collections;

public class Done_Homer : MonoBehaviour
{
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
		ArrayList hazards = gameController.GetHazards();
		//If there are no current hazards, return null.
		if (hazards.Count == 0)
			return null;
		GameObject closest = null;
		//Get closest hazard to lock onto
		foreach (GameObject item in hazards) {
			if (item != null) {
				//Check COV
				Vector3 direction = item.transform.position - transform.position;
				if (Vector3.Angle(transform.forward, direction) <= visionAngle)
				{
					//Replace 'closest' if null or 'item' is closer.
					if (closest == null || 
					    (closest.transform.position - transform.position).magnitude <
					    (item.transform.position - transform.position).magnitude) 
					{
						closest = item;
					}
				}
			}
		}
		if (closest == null)
						return null;
		return closest.transform;
	
	}
}
