﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOpenClose : MonoBehaviour {

	[SerializeField] private Transporter[] gates;
	private Animator anim;

	public void Start()
	{
		anim = GetComponent<Animator>();
	}

	void FlipSwitch()
	{
		if (anim.GetBool("switchActive")) {
			anim.SetBool("switchActive", false);
			for (int i = 0; i < gates.Length; ++i)
				gates[i].Close();
			StartCoroutine(GetComponentInParent<CameraManager>().CameraShake());
		}
		else {
			anim.SetBool("switchActive", true);
			for (int i = 0; i < gates.Length; ++i)
				gates[i].Open();
			StartCoroutine(GetComponentInParent<CameraManager>().CameraShake());
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player" && Input.GetKeyDown("space")) {
			FlipSwitch();
		}
	}
}
