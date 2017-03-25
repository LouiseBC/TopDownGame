using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour {

	public Transporter otherGate;
	public Transporter alternateGate;
	private Animator anim;

	[SerializeField] private BoxCollider2D triggerCollider; 
	[SerializeField] private bool isOpen = false;
	[SerializeField] private bool isLadder = false;

	public bool isBipolarLevelGate;
	public bool isLevelGate;

	//Awake is called when the script instance is being loaded.
	void Awake()
	{
		if (triggerCollider == null)
			triggerCollider = GetComponent<BoxCollider2D>();
		
		if (!isOpen)
			triggerCollider.isTrigger = false;
		anim = GetComponentInChildren<Animator>();
	}

	public void Open()
	{
		isOpen = true;
		anim.SetBool("switchActive", true);
		triggerCollider.isTrigger = true;
	}

	public void Close()
	{
		isOpen = false;
		anim.SetBool("switchActive", false);
		triggerCollider.isTrigger = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isOpen && other.tag == "Player" && otherGate != null) {
			if (isLadder) {
				other.gameObject.layer -= 1;
				GetComponentInParent<Loader>().SetFade();
				other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Entities-1";
			}
			if (isLevelGate) {
				isLevelGate = false; // Don't reuse this gate to load level.
				isBipolarLevelGate = false;
				GetComponentInParent<LevelManager>().LoadNextLevel();
			}
			other.SendMessage("Teleport", (Vector2)otherGate.transform.position);
		}
	}
}
