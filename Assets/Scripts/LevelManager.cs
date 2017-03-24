using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[SerializeField] private GameObject currentLevel;
	private GameObject nextLevel;

	void Start () 
	{
		nextLevel = currentLevel.GetComponent<Loader>().nextLevel;
	}

	public void LoadNextLevel()
	{
		if (nextLevel != null) {
			nextLevel.SetActive(true);
			currentLevel = nextLevel;
			nextLevel = currentLevel.GetComponent<Loader>().nextLevel;
		}
		
		Vector2 offset = currentLevel.GetComponent<Loader>().cameraOffset;
		if (offset != Vector2.zero)
			StartCoroutine(GetComponent<CameraManager>().PanCamera((Vector3)offset));
	}
}
