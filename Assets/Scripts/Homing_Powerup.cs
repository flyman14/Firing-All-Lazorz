using UnityEngine;
using System.Collections;

public class Homing_Powerup : Powerup {

	protected override void ApplyEffect(Done_PlayerController playerController)
	{
		playerController.bonusHoming++;
	}
}
