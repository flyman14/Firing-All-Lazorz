using UnityEngine;
using System.Collections;

public class FireratePowerup : Powerup {

	protected override void ApplyEffect(Done_PlayerController playerController)
	{
		playerController.fireRate *= 0.9f;
	}
}
