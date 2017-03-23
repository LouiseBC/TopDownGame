using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[SerializeField] private GameObject currentLevel;
	private GameObject nextLevel;
	[SerializeField] private float cameraSpeed = 1f;

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
		if (offset != Vector2.zero) {
			Vector3 newpos = Camera.main.transform.position + (Vector3)offset;
			StartCoroutine(PanCamera(newpos));
		}
	}

	IEnumerator PanCamera(Vector3 targetpos)
	{
		Vector3 oldpos = Camera.main.transform.position;
		float interpolation = 0f;
		while (Camera.main.transform.position != targetpos) {
			Vector3 increment = Vector3.Lerp(oldpos, targetpos, interpolation);
			Camera.main.transform.position = increment;
			interpolation += cameraSpeed * Time.deltaTime;
			yield return null;
		}
		yield return null;
	}
}
