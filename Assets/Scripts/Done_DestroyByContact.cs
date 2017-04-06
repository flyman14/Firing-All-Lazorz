using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
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
		if (other.tag == "Boundary")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			if (gameController.playerInvincible) {
				gameController.AddScore(scoreValue);
				Destroy (gameObject);
				return;
			} else {
				Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
				if (--gameController.playerLives <= 0) {
					gameController.GameOver();
				} else {
					gameController.Respawn();
				}
				gameController.livesText.text = "Lives: " + gameController.playerLives;
			}

		}
		
		gameController.AddScore(scoreValue);

		Destroy (other.gameObject);
		Destroy (gameObject);
	}


}