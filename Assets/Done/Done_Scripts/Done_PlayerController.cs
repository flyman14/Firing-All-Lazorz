using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	public int bonusHoming;
	public int bonusShots;
	 
	private float nextFire;
	private const float shotSpread = 0.5f;

	void Start ()
	{
		bonusHoming = 0;
	}
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			GameObject newBolt = (GameObject)Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			newBolt.GetComponent<Done_Homer>().turnSpeed = bonusHoming;
			audio.Play ();
			if (bonusShots > 0) {
				for (int i = 1; i <= bonusShots; i++) {
					newBolt = (GameObject)Instantiate(shot, shotSpawn.position + Vector3.left*i*shotSpread, shotSpawn.rotation);
					newBolt.GetComponent<Done_Homer>().turnSpeed = bonusHoming;
					newBolt = (GameObject)Instantiate(shot, shotSpawn.position + Vector3.right*i*shotSpread, shotSpawn.rotation);
					newBolt.GetComponent<Done_Homer>().turnSpeed = bonusHoming;
				}
			}
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = new Vector3
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}
