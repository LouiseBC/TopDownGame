using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour {

	[SerializeField] protected Transporter[] gates;
	protected Animator anim;

	protected virtual void Start()
	{
		anim = GetComponent<Animator>();
	}

	protected virtual void FlipSwitch()
	{
		if (anim.GetBool("switchActive")) {
			anim.SetBool("switchActive", false);
		}
		else {
			anim.SetBool("switchActive", true);
		}
	}

	protected abstract void OnTriggerStay2D(Collider2D other);
}
