//Taras Kravchuk
//CPSC 466 Game Programming
//CaveRunner
//Script for managing the players movement

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour {

	//Maximum speed of the player
	public float maxSpeed = 10f;
	public float xChange;
	//public float gravity = .5f;
	private bool updateOn = true;
	private PlayOneShotScript clip;

	//Whether the player is facing left or right
	//bool facingRight = true;

	//Amount of force to apply when jumping
	//public float jumpForce = 300f;

	//Reference to Rigidbody2D, so we don't need to try and call GetComponent every frame and every physics timestep
	Rigidbody2D myRigidbody;

	//Whether we're on the ground or not
	//bool grounded = false;

	private Animator anim;

	//The location where to check if the player has hit the ground
	//public Transform groundCheck;

	//How big the groundCheck should be
	//float groundRadius = 0.2f;

	//Determining what things are the ground or not
	//public LayerMask whatIsGround;


	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		//myRigidbody.gravityScale = gravity;
		//clip = GameObject.Find("AudioManager").GetComponent<PlayOneShotScript>();

	}
	
	// Update is called once per frame
	void Update () {
		/*if (updateOn == true) {
			if (Input.GetButtonDown ("Jump") && myRigidbody.gravityScale > 0) {
				Flip ();
				grounded = false;
				myRigidbody.AddForce (new Vector2 (0, jumpForce));
				myRigidbody.gravityScale = -gravity * rate();
			} else if (Input.GetButtonDown ("Jump") && myRigidbody.gravityScale < 0) {
				Flip ();
				grounded = false;
				myRigidbody.AddForce (new Vector2 (0, -jumpForce));
				myRigidbody.gravityScale = gravity * rate();
			}
		}*/

		if (updateOn == true) { // && Input.touchCount > 0    -- include in if stat. when using mobile platform

			//var touch = Input.GetTouch(0);     			  -- again, only for mobile
			if ((Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width/2))    //touch.position.x < Screen.width/2 || 
				{
					if (myRigidbody.transform.position.x > -xChange){
						myRigidbody.transform.position = new Vector2 (transform.position.x-.3f, transform.position.y);
					//transform.position.x -= 1;
					//DoLeftSideStuff();
					}
				}
			else if ((Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width/2))  //touch.position.x > Screen.width/2 ||
				{
					if(myRigidbody.transform.position.x < xChange){
						myRigidbody.transform.position = new Vector2 (transform.position.x+.3f, transform.position.y);
					//DoRightSideStuff();
					}
				}
		}

	}


	/*float rate() {
		if (ScoreManager.score < 20) {
			return 1f;
		} else if (ScoreManager.score < 50) {
			return 1.5f;
		} else if (ScoreManager.score < 100) {
			return 2.5f;
		} else {
			return 3f;
		}			
	}*/
	/*
	// Update is called once per *physics timestep*
	void FixedUpdate() {

		//Determining if the ground check spot has hit anywhere (on the ground)
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		anim.SetBool ("grounded", grounded);

	}

	//Called when we want to flip our player from pointing one direction to the other
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;

		// Flip the graphic using scale
		theScale.y *= -1; 
		transform.localScale = theScale;
	}


	//Called on collision
	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.name.StartsWith("Bat") || other.gameObject.name.StartsWith("RockT") 
			|| other.gameObject.name.StartsWith("RockB")){
			anim.SetBool ("hit", true);

			GameManager.Speed = 0;

			if (!other.gameObject.name.StartsWith ("Bat")) {
				myRigidbody.transform.position = new Vector2 (transform.position.x-.3f, transform.position.y);



			}
			clip.SetVolume(.25f);
			clip.PlayOneShot (3);
			myRigidbody.gravityScale = 5;
			updateOn = false;
			StartCoroutine(delay());

		}
	}

    //Delay to allow time for animation to play before end of level
	IEnumerator delay(){

		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene ("Menu");

	}

	*/

}
