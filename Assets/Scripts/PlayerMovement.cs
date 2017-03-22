using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private float moveSpeed;
	private float x;
	private float y;

	private Rigidbody2D rb;

	public IEnumerator Teleport(Vector2 newposition)
	{
		print("Starting");
		GetComponent<BoxCollider2D>().enabled = false;
		transform.position = newposition;
		Vector2 targetpos = newposition;
		targetpos.y -= 0.16f;
		print(transform.position.x + " " + transform.position.y);

		while ((Vector2)transform.position != targetpos) {
			print("Doingstuff");
			transform.position = Vector2.MoveTowards(transform.position, targetpos, 0.25f*moveSpeed*Time.deltaTime);
			//rb.MovePosition(newpos);
			yield return null;
		}
		GetComponent<BoxCollider2D>().enabled = true;
		yield return null;
	}

	void Start()
	{
		moveSpeed = 2f;
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		x = Input.GetAxisRaw("Horizontal");
		y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		if (x != 0 || y != 0) {
			Vector2 targetpos = rb.position;

			if (x != 0)
				targetpos.x += 1 * x;
			else if (y != 0)
				targetpos.y += 1 * y;

			Vector2 newPos = Vector2.MoveTowards(rb.position, targetpos, Time.deltaTime * moveSpeed);
			rb.MovePosition(newPos);
		}
	}
}
