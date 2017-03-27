using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public Vector2 cameraOffset;
	[SerializeField] private bool fadedIn = false;
	private SpriteRenderer[] renderers;
	public GameObject nextLevel;


	void OnEnable()
	{
		if (!fadedIn)
			StartCoroutine(FadeIn());
	}

	IEnumerator FadeIn()
	{
		renderers = GetComponentsInChildren<SpriteRenderer>();
		if (renderers.Length < 1) yield break;

		Color fadein = new Color(1, 1, 1, 1);

		// Set every sprite to invisible
		for (int i = 0; i < renderers.Length; ++i) {
			renderers[i].color -= fadein;
		}
		yield return null;

		// Slowly fade in
		while (renderers[0].color != fadein) {
			Color next = Color.Lerp(renderers[0].color, fadein, 0.05f);
			for (int i = 0; i < renderers.Length; ++i) {
				renderers[i].color = next;
			}
			yield return null;
		}
		yield return null;
	}

	public void SetFade()
	// Assumes renderers have already been loaded successfully
	// If objects are added during gameplay we will have to reload
	{
		Color faded = new Color(0, 0, 0, 0.5f);
		int inOrOut = (renderers[0].color.a < 1) ? 1 : -1;

		for (int i = 0; i < renderers.Length; ++i) {
				renderers[i].color -= faded * inOrOut;
		}
	}

		Color target = overlay.color;
		target.a = 0f;
		while (overlay.color.a > target.a) {
			overlay.color = Color.Lerp(overlay.color, target, 0.05f);
			yield return null;
		}
		fadedIn = true;
		yield return null;
	}
}
