using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour {

	public Transporter otherGate;
	[SerializeField] private BoxCollider2D triggerCollider; // the collider that is a trigger, may be set to null
	private Animator anim;
	private bool isOpen;
	
	private Vector2 spawnPoint;
	[SerializeField] private bool isLevelGate;

	public void Open()
	{
		isOpen = true;
		anim.SetBool("switchActive", true);
	}

	public void Close()
	{
		isOpen = false;
		anim.SetBool("switchActive", false);
	}

	//Awake is called when the script instance is being loaded.
	void Awake()
	{
		isOpen = false;
		spawnPoint = this.transform.position;
		anim = GetComponentInChildren<Animator>();
		if (triggerCollider == null) {
			triggerCollider = GetComponent<BoxCollider2D>();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		print("Triggered Transporter");
		if (/*isOpen &&*/ other.tag == "Player" && otherGate != null) {
			if (isLevelGate) {
				isLevelGate = false; // Don't reuse this gate to load level.
				GetComponentInParent<LevelManager>().LoadNextLevel();
			}
			other.SendMessage("Teleport", otherGate.spawnPoint);	
		}
	}
}
