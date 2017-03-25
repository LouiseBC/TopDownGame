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

	private Rigidbody2D rb;
	private SpriteRenderer ren;
	private BoxCollider2D collider;

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
		
		collider.enabled = false;
		while ((Vector2)transform.position != targetpos) {
			transform.position = Vector2.MoveTowards(transform.position, targetpos, 0.5f*Time.deltaTime);
			yield return null;
		}
		collider.enabled = true;
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
		collider = GetComponent<BoxCollider2D>();
	}

	void Update()
	{
		x = Input.GetAxisRaw("Horizontal");
		y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		if (!isMoving && (x != 0 || y != 0)) {
			Vector2 targetpos = rb.position;

			if (x != 0)
				targetpos.x += moveStep * x;
			else if (y != 0)
				targetpos.y += moveStep * y;

			if (CanMove(targetpos))
				currMove = StartCoroutine(Move(targetpos));
			else print("cantmove");
		}
	}

	bool CanMove(Vector2 targetpos)
	{
		collider.enabled = false;

		Vector3 origin = transform.position;
		origin.y += collider.offset.y;

		float xAdd = (x != 0) ? (collider.size.x * 0.5f * x) : 0f;
		float yAdd = (y != 0) ? (collider.size.y * 0.5f * y) : 0f;
		Vector3 target = (Vector3)targetpos + new Vector3(xAdd, collider.offset.y + yAdd, 0);

		RaycastHit2D hit = Physics2D.Linecast(origin, target);
		Debug.DrawLine(origin, target, Color.white, 3f);

		collider.enabled = true;

		return (hit.transform == null || (hit.collider.isTrigger == true));
	}

	IEnumerator Move(Vector2 targetpos)
	{
		isMoving = true;
		while ((Vector2)rb.transform.position != targetpos) {
			Vector2 newpos = Vector2.Lerp(transform.position, targetpos, Time.deltaTime * moveSpeed);
			//Vector2 newpos = Vector2.MoveTowards(rb.transform.position, targetpos, Time.deltaTime*moveSpeed);;
			rb.MovePosition(newpos);
			//transform.position = Vector2.MoveTowards(rb.transform.position, targetpos, Time.deltaTime*moveSpeed); //newpos;
			yield return null;
		}
		isMoving = false;
		yield return null;
	}
}
