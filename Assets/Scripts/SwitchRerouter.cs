﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRerouter : Switch {

	protected override void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player" && Input.GetKeyDown("space")) {
			FlipSwitch();
			RerouteGates();
			GetComponent<AudioSource>().Play();
		}
	}

	void RerouteGates()
	{
		for (int i = 0; i < gates.Length; ++i) {

			if (gates[i].alternateGate == null) continue;

			if (gates[i].isBipolarLevelGate)
				gates[i].isLevelGate = !gates[i].isLevelGate;

			Transporter temp = gates[i].otherGate;
			gates[i].otherGate = gates[i].alternateGate;
			gates[i].alternateGate = temp;
		}
	}

}
