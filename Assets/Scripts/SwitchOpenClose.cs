using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOpenClose : Switch {

	protected override void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player" && Input.GetKeyDown("space")) {
			FlipSwitch();
			OpenClose();
		}
	}

	void OpenClose()
	{
		if (anim.GetBool("switchActive")) {
			for (int i = 0; i < gates.Length; ++i)
				gates[i].Open();
			StartCoroutine(GetComponentInParent<CameraManager>().CameraShake());
		}
		else {
			for (int i = 0; i < gates.Length; ++i)
				gates[i].Close();
			StartCoroutine(GetComponentInParent<CameraManager>().CameraShake());
		}
	}
}
