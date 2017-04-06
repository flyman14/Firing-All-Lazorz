using UnityEngine;
using System.Collections;

public class Done_GameController : MonoBehaviour
{
	public HazardEntry[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int playerLives;
	public float respawnWait;
	public float invincibleWait;
	public bool playerInvincible;
	public GameObject player;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText livesText;

	private int totalWeight;
	private bool gameOver;
	private bool restart;
	private int score;
	private ArrayList currentHazards;

	private const byte targetCheckWait = 120;
	private byte checkTime;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		currentHazards = new ArrayList ();
		checkTime = 0;
		playerLives = 3;
		livesText.text = "Lives: " + playerLives;
		StartCoroutine (SpawnWaves ());
		totalWeight = 0;
		foreach (HazardEntry entry in hazards) 
		{
			totalWeight += entry.weight;
		}
	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetButton("Submit"))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		if (checkTime >= targetCheckWait) {
			ClearHazards();
			checkTime = 0;
				}
		checkTime++;
		if (Input.GetButton("Cancel"))
		{
			Application.LoadLevel (0);
		}
	}

	//Removes null objects from hazards
	void ClearHazards()
	{
		foreach (GameObject item in currentHazards) {
			if (item == null) {
				currentHazards.Remove(item);

				return;
			}
				}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = null;
				int randomChoice = Random.Range(0, totalWeight);
				foreach (HazardEntry entry in hazards) {
					if (randomChoice < entry.weight) 
					{
						hazard = entry.hazard;
						break;
					}
					randomChoice -= entry.weight;
				}

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				hazard = (GameObject)Instantiate (hazard, spawnPosition, spawnRotation);
				currentHazards.Add(hazard);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			hazardCount++;
			spawnWait *= 0.9f;
			
			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}



	public void Respawn()
	{
		StartCoroutine (RespawnPlayer ());
	}

	IEnumerator RespawnPlayer ()
	{
		//Wait a few seconds before respawning.
		Debug.Log ("Respawn player active");
		yield return new WaitForSeconds (respawnWait);
		Debug.Log ("Player Instantiating");
		Instantiate (player, Vector3.zero, Quaternion.identity);
		playerInvincible = true;
		yield return new WaitForSeconds (invincibleWait);
		playerInvincible = false;
	}

	public ArrayList GetHazards()
	{
		return currentHazards;
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		restartText.text = "Press 'R' for Restart";
		restart = true;
	}


}

[System.Serializable]
public struct HazardEntry
{
	public GameObject hazard;
	public int weight;
}