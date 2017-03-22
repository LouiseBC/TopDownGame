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

	void Start()
	{
		spawnPoint = this.transform.position;
		spawnPoint.y -= 0.08f;
		anim = GetComponentInChildren<Animator>();
		if (triggerCollider == null) {
			triggerCollider = GetComponent<BoxCollider2D>();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		print("Triggered");
		if (other.tag == "Player" && otherGate != null) {
			other.SendMessage("Teleport", otherGate.spawnPoint);
			other.transform.position = otherGate.spawnPoint;
			StartCoroutine(otherGate.SetToTrigger());

			if (isLevelGate) {
				//gameManager.LoadLevels();
			}
		}
	}

	IEnumerator SetToTrigger()
	{
		// change trigger to collider so can't immediately return
		triggerCollider.isTrigger = false;
		yield return new WaitForSeconds(0.5f);

		triggerCollider.isTrigger = true;
		yield return null;
	}
}
