using UnityEngine;
using System.Collections;

public class HowToMagicScript : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	bool facingRight = true;
	float move;

	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public bool grounded;
	// Use this for initialization
	void Start () {
	
	}
	void FixedUpdate() {
		move = Input.GetAxis ("Horizontal");
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
	}
	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetKeyDown (KeyCode.UpArrow))
						GetComponent<Rigidbody2D>().AddForce (new Vector2 (0f, jumpForce));
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (facingRight && move < 0)
						Flip ();
				else if (move > 0 && !facingRight)
						Flip ();
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		}
}
