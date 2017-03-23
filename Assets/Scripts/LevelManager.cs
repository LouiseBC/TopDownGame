﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	[SerializeField] private GameObject currentLevel;
	[SerializeField] private GameObject nextLevel;

	void Start () 
	{
		nextLevel = currentLevel.GetComponent<Loader>().nextLevel;
	}

	public void LoadNextLevel()
	{
		print("Loadlevel");
		if (nextLevel != null) {
			nextLevel.SetActive(true);
			currentLevel = nextLevel;
			nextLevel = currentLevel.GetComponent<Loader>().nextLevel;
		}
	}
}
