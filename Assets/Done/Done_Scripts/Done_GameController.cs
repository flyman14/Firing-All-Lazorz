using UnityEngine;
using System.Collections;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
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
	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		if (checkTime >= targetCheckWait) {
			ClearHazards();
			checkTime = 0;
				}
		checkTime++;
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
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				hazard = (GameObject)Instantiate (hazard, spawnPosition, spawnRotation);
				currentHazards.Add(hazard);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
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