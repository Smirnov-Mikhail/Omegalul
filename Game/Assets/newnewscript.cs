using UnityEngine;
using System.Collections;

public class newnewscript : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	public float move;
	bool facingRight = true;

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
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (facingRight && move < 0)
						Flip ();
				else if (move > 0 && !facingRight)
						Flip ();

		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded)
						GetComponent<Rigidbody2D>().AddForce (new Vector2 (0f, jumpForce));

		if (Input.GetKeyDown (KeyCode.R))
						Application.LoadLevel (Application.loadedLevel);
	}

	void Flip(){

		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Star")
						Destroy (col.gameObject);
	}
}
