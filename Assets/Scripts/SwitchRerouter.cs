using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRerouter : MonoBehaviour {

	[SerializeField] private Transporter[] gates;
	private Animator anim;

	public void Start()
	{
		anim = GetComponent<Animator>();
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player" && Input.GetKeyDown("space")) {
			FlipSwitch();
			RerouteGates();
		}
	}

	void FlipSwitch()
	{
		if (anim.GetBool("switchActive")) {
			anim.SetBool("switchActive", false);
		}
		else {
			anim.SetBool("switchActive", true);
		}
	}

	void RerouteGates()
	{
		for (int i = 0; i < gates.Length; ++i) {

			if (gates[i].isBipolarLevelGate)
				gates[i].isLevelGate = !gates[i].isLevelGate;

			if (gates[i].alternateGate == null) continue;

			Transporter temp = gates[i].otherGate;
			gates[i].otherGate = gates[i].alternateGate;
			gates[i].alternateGate = temp;
		}
	}

}
