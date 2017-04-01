using UnityEngine;
using System.Collections;

public class Multishot_Powerup : Powerup {

	private const int maxStack = 4;

	protected override void ApplyEffect(Done_PlayerController playerController)
	{
		playerController.bonusShots = Mathf.Clamp(playerController.bonusShots+1,0,maxStack);
	}
}
