using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	[SerializeField] private bool fadedIn = false;
	[SerializeField] private SpriteRenderer overlay;
	public GameObject nextLevel;

	void Update()
	{
		if (!fadedIn)
			FadeIn();
	}

	void FadeIn()
	{
		overlay.color = Color.Lerp(overlay.color, new Color(0, 0, 0, 0), 0.05f);
		if (overlay.color.a == 0f)
			fadedIn = true;
	}
}
