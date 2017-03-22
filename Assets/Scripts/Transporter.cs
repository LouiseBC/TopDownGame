using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour {

	public Transporter otherGate;
	[SerializeField] private BoxCollider2D triggerCollider; // the collider that is a trigger, may be set to null
	private Animator anim;
	
	private Vector2 spawnPoint;
	[SerializeField] private bool isLevelGate;

	public void Open()
	{
		anim.SetBool("switchActive", true);
		
	}

	public void Close()
	{
		anim.SetBool("switchActive", false);
		
	}

	//Awake is called when the script instance is being loaded.
	void Awake()
	{
		spawnPoint = this.transform.position;
		spawnPoint.y += 0.04f; // Start off player slightly (0.25 unit) behind gate
		anim = GetComponentInChildren<Animator>();
		if (triggerCollider == null) {
			triggerCollider = GetComponent<BoxCollider2D>();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		print("Triggered");
		if (other.tag == "Player" && otherGate != null) {
			if (isLevelGate) {
				isLevelGate = false; // Don't reuse this gate to load level.
				GetComponentInParent<LevelManager>().LoadNextLevel();
				//	otherGate.triggerCollider.isTrigger = false; // ensure the player cant return to previous level
			}
			print(otherGate.transform.position.x + " " + otherGate.transform.position.y);
			//StartCoroutine(other.Teleport(otherGate.spawnPoint));
			other.SendMessage("Teleport", otherGate.spawnPoint);
			//StartCoroutine(otherGate.TempDisableTrigger());		
		}
	}

	IEnumerator TempDisableTrigger()
	{
		// change trigger to collider so can't immediately return
		triggerCollider.isTrigger = false;
		yield return new WaitForSeconds(0.5f);

		triggerCollider.isTrigger = true;
		yield return null;
	}
}
