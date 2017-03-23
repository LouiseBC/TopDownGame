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
		print("heythere " + fadedIn);
	}

	IEnumerator FadeIn()
	{
		print("fading " + Time.deltaTime);
		Color target = overlay.color;
		target.a = 0f;
		while (overlay.color.a > target.a) {
			overlay.color = Color.Lerp(overlay.color, target, 0.05f);
			yield return null;
		}
		fadedIn = true;
		print("done");
		yield return null;
	}
}
