using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	[SerializeField] private bool fadedIn = false;
	private SpriteRenderer overlay;
	public GameObject nextLevel;

	void Start () 
	{
		overlay = GetComponent<SpriteRenderer>();
	}

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
