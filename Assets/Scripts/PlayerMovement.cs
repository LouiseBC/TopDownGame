using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float moveSpeed;
	private float moveStep;
	private bool isMoving;
	private float x;
	private float y;

	private Rigidbody2D rb;

	public void Teleport(Vector2 newposition)
	{
		isMoving = true;
		//ren.color.a = 0;
		transform.position = newposition;
		/*
		yield return new WaitForSeconds(0.01f);
		while (ren.color.a < 1) {
			ren.color.a += 0.01;
			yield return null;
		}*/
		isMoving = false;
		//yield return null;
	}

	void Start()
	{
		moveStep = 0.16f;
		moveSpeed = 10f;//10f;
		isMoving = false;

		rb = GetComponent<Rigidbody2D>();
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

			Vector2 newPos = Vector2.MoveTowards(rb.position, targetpos, moveStep * Time.deltaTime * moveSpeed); // Move by moveStep amount, moderated by deltaTime and moveSpeed
			rb.MovePosition(newPos);
		}
	}
}
