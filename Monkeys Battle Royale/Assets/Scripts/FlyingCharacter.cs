using UnityEngine;
using System.Collections;

public class FlyingCharacter : MonoBehaviour {
    private Rigidbody2D body;
	private Animator animator;
    private int fuel;
	public Transform groundDetector;
	public LayerMask groundMask;

	private bool onGround;

	void Start () {
		animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        fuel = 1000;
		onGround = false;
		//transform.localScale.Set (-1, 1, 1);
		//transform.Rotate(new Vector3 (0, 0, 90));
		//transform.lossyScale.Scale(new Vector3 (-1, 1, 1));
	}

	void Update() {
		/*
		 * MOVE TO FU?
		 * */

		float xInput = Input.GetAxis("Horizontal");
		float yInput = Input.GetAxis("Vertical");
		float velocityY = body.velocity.y;
		if(yInput > 0 && fuel > 0)
		{
			fuel--;
			velocityY = yInput * 2;
		}
		body.velocity = new Vector2(xInput * 10, velocityY);

		onGround = Physics2D.OverlapCircle(new Vector2(groundDetector.position.x, groundDetector.position.y), 0.05f, groundMask);
		//END MOVE TO FU
		animator.SetBool ("impulsing", Mathf.Abs(xInput) > 0);



		if (xInput > 0) {
			transform.localScale = new Vector3(1, 1, 1);
		} else if (xInput < 0) {
			transform.localScale = new Vector3(-1, 1, 1);
		}



		animator.SetBool ("onGround", onGround);
		//animator.SetFloat ("speedX", Mathf.Abs (body.velocity.x));
	}

	void FixedUpdate () {

	}
}
