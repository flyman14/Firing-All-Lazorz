using UnityEngine;
using System.Collections;

/*
 * Abstract class for powerups. 
 */
public abstract class Powerup : MonoBehaviour {

	//Apply a powerup to the player
	public void GivePowerup(GameObject player)
	{
		Done_PlayerController playerController = player.GetComponent<Done_PlayerController> ();
		ApplyEffect (playerController);
	}

	protected abstract void ApplyEffect(Done_PlayerController playerController);
}
