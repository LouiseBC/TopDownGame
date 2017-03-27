using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour {

	public Transporter otherGate;
	public Transporter alternateGate;

	[SerializeField] private BoxCollider2D triggerCollider; 
	[SerializeField] private bool isOpen = false;
	[SerializeField] private bool isLadder = false;

	public bool isBipolarLevelGate;
	public bool isLevelGate;

	[SerializeField] AudioSource openAudio;
	[SerializeField] AudioSource closeAudio;
	private Animator anim;

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
		openAudio.PlayDelayed(0.1f);
	}

	public void Close()
	{
		isOpen = false;
		anim.SetBool("switchActive", false);
		triggerCollider.isTrigger = false;
		closeAudio.PlayDelayed(0.1f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isOpen && other.tag == "Player" && otherGate != null) {
<<<<<<< HEAD
			if (isLadder) {
				other.gameObject.layer -= 1;
				GetComponentInParent<Loader>().SetFade();
				other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Entities-1";
			}
=======
>>>>>>> a47a30e66b8dbc093e5352f31ea76e8989c0139d
			if (isLevelGate) {
				isLevelGate = false; // Don't reuse this gate to load level.
				isBipolarLevelGate = false;
				GetComponentInParent<LevelManager>().LoadNextLevel();
			}
			other.SendMessage("Teleport", (Vector2)otherGate.transform.position);
		}
	}
}
