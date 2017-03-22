using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private bool isTeleporting;
	private float moveSpeed;
	private float x;
	private float y;

	private Rigidbody2D rb;
	private SpriteRenderer ren;

	public IEnumerator Teleport(Vector2 newposition)
	{
		// Reposition Player
		isTeleporting = true;
		GetComponent<BoxCollider2D>().enabled = false;
		transform.position = newposition;

		Vector2 targetpos = newposition;
		targetpos.y -= 0.16f;

		StartCoroutine(FadeIn());

		while ((Vector2)transform.position != targetpos) {
			transform.position = Vector2.MoveTowards(transform.position, targetpos, 0.25f*moveSpeed*Time.deltaTime);
			yield return null;
		}
		GetComponent<BoxCollider2D>().enabled = true;
		isTeleporting = false;
		yield return null;
	}

	IEnumerator FadeIn()
	{
		// Fade in from black
		Color target = ren.color;
		Color faded = new Color(0, 0, 0, 0);
		ren.color = faded;

		while (ren.color != target) {
			ren.color = Color.Lerp(ren.color, target, 0.05f);
			yield return null;
		}
		yield return null;
	}

	void Start()
	{
		isTeleporting = false;
		moveSpeed = 2f;
		rb = GetComponent<Rigidbody2D>();
		ren = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		x = Input.GetAxisRaw("Horizontal");
		y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		if (!isTeleporting && (x != 0 || y != 0)) {
			Vector2 targetpos = rb.position;

			if (x != 0)
				targetpos.x += 1 * x;
			else if (y != 0)
				targetpos.y += 1 * y;

			Vector2 newpos = Vector2.MoveTowards(rb.position, targetpos, Time.deltaTime * moveSpeed);
			rb.MovePosition(newpos);
		}
	}
}
