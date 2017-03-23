using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	[SerializeField] private bool fadedIn = false;
	[SerializeField] private SpriteRenderer overlay;
	public GameObject nextLevel;


	void OnEnable()
	{
		if (!fadedIn)
			StartCoroutine(FadeIn());
	}

	void Update()
	{
		//if (!fadedIn)
		//	FadeIn();
	}

	IEnumerator FadeIn()
	{
		Color target = overlay.color;
		target.a = 0f;
		while (overlay.color.a > target.a) {
			overlay.color = Color.Lerp(overlay.color, target, 0.05f);
			yield return null;
		}
		fadedIn = true;
		yield return null;
	}

	void FadeInn()
	{
		overlay.color = Color.Lerp(overlay.color, new Color(0, 0, 0, 0), 0.05f);
		if (overlay.color.a == 0f)
			fadedIn = true;
	}
}
