using UnityEngine;
using System.Collections;


public class LoadSceneOnClick : MonoBehaviour {

	public void loadByIndex(int sceneIndex)
	{
		Application.LoadLevel (sceneIndex);
	}
}
