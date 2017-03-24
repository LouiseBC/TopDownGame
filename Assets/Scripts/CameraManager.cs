using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	[SerializeField] private float cameraSpeed = 1f;
	[SerializeField] private float shakeOffset = 0.1f;

	public IEnumerator PanCamera(Vector3 offset)
	{
		Vector3 oldpos = Camera.main.transform.position;
		Vector3 targetpos = oldpos + offset;
		
		float interpolation = 0f;

		while (Camera.main.transform.position != targetpos) {
			Vector3 increment = Vector3.Lerp(oldpos, targetpos, interpolation);
			Camera.main.transform.position = increment;
			interpolation += cameraSpeed * Time.deltaTime;
			yield return null;
		}
		yield return null;
	}

	public IEnumerator CameraShake()
	{
		Vector3 initialpos = Camera.main.transform.position;
		float duration = 0.5f;
		bool shake = true;		// Shake every other frame

		while (duration > 0) {
			if (shake) {
				Vector3 shakepos = initialpos + (Vector3)(Random.insideUnitCircle * shakeOffset);
				shakepos.x = initialpos.x;
				Camera.main.transform.position = shakepos;
				duration -= Time.deltaTime;
			}
			shake = !shake;
			yield return null;
		}
		Camera.main.transform.position = initialpos;
		yield return null;
	}
}
