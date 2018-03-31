﻿using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class characterController : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	bool facingRight = true;
	bool grounded = false;
	public Transform groundCheck;
	public float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float score;
	public float move;
    public bool locked;

	private GameObject star;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		move = Input.GetAxis ("Horizontal");

	}

	void Update()
    {
        if (locked)
            return;

		if (grounded && (Input.GetKeyDown (KeyCode.W)||Input.GetKeyDown (KeyCode.UpArrow))) {

			GetComponent<Rigidbody2D>().AddForce (new Vector2(0f,jumpForce));
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();



		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKey(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}


	}
	
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D col){
		if ((col.gameObject.name == "dieCollider")||(col.gameObject.name == "saw"))
						Application.LoadLevel (Application.loadedLevel);

	    if (col.gameObject.name == "star") {
						score++;
						Destroy (col.gameObject);
				}

        if (col.gameObject.name == "DangerBurger")
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (col.gameObject.name == "Spring")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 500));
        }

        if (col.gameObject.name == "endLevel")
        {
            StatisticData.instance.FinishLevels++;
        }

        if (col.gameObject.name == "nextLevel")
        {
            switch (StatisticData.instance.FinishLevels)
            {
                case 0:
                    Application.LoadLevel("save-load");
                    return;
                case 1:
                    Application.LoadLevel("ViuginickScene");
                    return;
                case 2:
                    Application.LoadLevel("save-load");
                    return;
                case 3:
                    Application.LoadLevel("save-load");
                    return;
                default:
                    Application.LoadLevel(Application.loadedLevel);
                    return;

            }
            /*if (!(GameObject.Find("star"))) Application.LoadLevel ("save-load");
				}*/
        }

        if (col.gameObject.name == "finishCollider" && !StatisticData.instance.EndButtonIsActive)
            Application.LoadLevel("gameOver");
    }

	void OnGUI(){
		//GUI.Box(new Rect (0, 0, 100, 100), "Stars: " + score);
        GUI.Box(new Rect(Screen.width / 7, Screen.height - 100, Screen.width - Screen.width * 2 / 7, 100), "Sample text");
    }

}
