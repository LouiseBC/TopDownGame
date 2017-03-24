// new verison

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private bool isMoving;
	private float moveStep;
	[SerializeField] private float moveSpeed;
	private float x;
	private float y;
	private Coroutine currMove;
	private Vector2 targetPos;

	private Rigidbody2D rb;
	private SpriteRenderer ren;

	public IEnumerator Teleport(Vector2 newposition)
	{
		// Reposition Player
		if (currMove != null)
			StopCoroutine(currMove);
		transform.position = newposition;

		Vector2 targetpos = newposition;
		targetpos.y -= moveStep*2;

		StartCoroutine(FadeIn());
		isMoving = true;
		
		GetComponent<BoxCollider2D>().enabled = false;
		while ((Vector2)transform.position != targetpos) {
			transform.position = Vector2.MoveTowards(transform.position, targetpos, 0.5f*Time.deltaTime);
			yield return null;
		}
		GetComponent<BoxCollider2D>().enabled = true;
		isMoving = false;
		yield return null;
	}

	IEnumerator FadeIn()
	{
		// Fade in from black
		Color target = new Color(1, 1, 1, 1);
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
		isMoving = false;
		moveStep = 0.08f;
		moveSpeed = 40f;
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
		if (!isMoving && (x != 0 || y != 0)) {
			targetPos = rb.position;

			if (x != 0)
				targetPos.x += moveStep * x;
			else if (y != 0)
				targetPos.y += moveStep * y;

			if (CanMove(targetPos))
				currMove = StartCoroutine(Move());
		}
	}

	bool CanMove(Vector2 targetpos)
	{
		GetComponent<BoxCollider2D>().enabled = false;
		RaycastHit2D hit = Physics2D.Linecast(transform.position, targetpos, transform.gameObject.layer);
		GetComponent<BoxCollider2D>().enabled = true;

		return (hit.transform == null);
	}

	IEnumerator Move()
	{
		isMoving = true;
		while ((Vector2)rb.transform.position != targetPos) {
			Vector2 newpos = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
			transform.position = newpos;
			yield return null;
		}
		isMoving = false;
		yield return null;
	}



}
