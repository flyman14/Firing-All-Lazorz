using UnityEngine;
using System.Collections;

public class DestroyPowerupOnContact : MonoBehaviour {

	public GameObject explosion;

	public int scoreValue;
	
	private Done_GameController gameController;
	
	
	
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
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Bolt")
		{
			return;
		}
		
		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}
		
		if (other.tag == "Player")
		{
			//Give player the power
			gameObject.GetComponent<Powerup>().GivePowerup(other.gameObject);
			gameController.AddScore(scoreValue);
			Destroy (gameObject);
			return;
		}
		
		gameController.AddScore(scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
