//Taras Kravchuk
//CPSC 466 Game Programming
//CaveRunner
//Script for managing the players movement

using UnityEngine;
using UnityEngine.UI;
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
	public float timer = 61;
	Text timerText;
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		//myRigidbody.gravityScale = gravity;
		//clip = GameObject.Find("AudioManager").GetComponent<PlayOneShotScript>();
		GameObject eScore = GameObject.Find ("timer");
		timerText = eScore.GetComponent <Text> ();

		if (ScoreManager.energy - 3 < 0 || ScoreManager.rest - 2 < 0 || ScoreManager.money - 3 < 0 || ScoreManager.muscle - 3 < 0) {
			exitScene();
		}
		rate ();
	}
	
	// Update is called once per frame

	void Update () {
		//stageChange = ScoreManager.stage;

		rate ();
		if (stageChange != ScoreManager.stage) {
			stageChange = ScoreManager.stage;
			rate ();
			//onGUI ();
			StartCoroutine(delay());
			exitScene ();
		}
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

		timer -= Time.deltaTime;
		if (timer < 0) {

			exitScene ();
		}
		timerText.text = "Timer: " + (int)timer;

		if (updateOn == true) { // && Input.touchCount > 0    -- include in if stat. when using mobile platform

			//var touch = Input.GetTouch(0);     			  -- again, only for mobile
			if ((Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width/2)  || Input.GetKey("left"))    //touch.position.x < Screen.width/2 || 
				{
					if (myRigidbody.transform.position.x > -xChange){
						myRigidbody.transform.position = new Vector2 (transform.position.x-.3f, transform.position.y);
					//transform.position.x -= 1;
					//DoLeftSideStuff();
					}
				}
			else if ((Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width/2) || Input.GetKey("right"))  //touch.position.x > Screen.width/2 ||
				{
					if(myRigidbody.transform.position.x < xChange){
						myRigidbody.transform.position = new Vector2 (transform.position.x+.3f, transform.position.y);
					//DoRightSideStuff();
					}
			}
		}

	}

	/*void onGUI(){
		GUI.Label (new Rect (0, 0, Screen.width / 2, Screen.height / 2), "Your stage has changed to Stage: "+ScoreManager.stage );
	}*/

	public static int stageChange {
		get {
			return PlayerPrefs.GetInt ("stageChange");
		}
		set{
			PlayerPrefs.SetInt ("stageChange", value);
		}
	}

	void rate() {
		if (ScoreManager.energy >= 200
		    && ScoreManager.muscle >= 200
		    && ScoreManager.rest >= 200
		    && ScoreManager.money >= 200
			&& ScoreManager.energy < 300
			&& ScoreManager.muscle < 300
			&& ScoreManager.rest < 300
			&& ScoreManager.money < 300) {
			//return 1.3f;
			//AssetPoolerScript.current.setSpeed (4.5f);
			//AssetMovementScript.current.AssetSpeed = 14.3f;
			ScrollFloor.current.scrollSpeed = 4.5f;
			ScoreManager.stage = 2;
			//exitScene ();
		} else if (ScoreManager.energy >= 300
		           && ScoreManager.muscle >= 300
		           && ScoreManager.rest >= 300
		           && ScoreManager.money >= 300
					&& ScoreManager.energy < 400
					&& ScoreManager.muscle < 400
					&& ScoreManager.rest < 400
					&& ScoreManager.money < 400) {
			//return 1.6f;
			//AssetPoolerScript.current.setSpeed (6f);
			ScrollFloor.current.scrollSpeed = 6f;
			ScoreManager.stage = 3;
			//exitScene ();


		} else if (ScoreManager.energy >= 400
		           && ScoreManager.muscle >= 400
		           && ScoreManager.rest >= 400
		           && ScoreManager.money >= 400
			&& ScoreManager.energy < 500
			&& ScoreManager.muscle < 500
			&& ScoreManager.rest < 500
			&& ScoreManager.money < 500) {
			//return 2.1f;
			//AssetPoolerScript.current.setSpeed (8f);
			ScrollFloor.current.scrollSpeed = 8f;
			ScoreManager.stage = 4;
			//exitScene ();


		} else {
			//AssetPoolerScript.current.setSpeed (3f);
			ScrollFloor.current.scrollSpeed = 3f;
			ScoreManager.stage = 1;
			//exitScene ();
		}
	}
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

	*/
	//Called on collision
	void OnCollisionEnter2D(Collision2D other) {

		other.gameObject.SetActive (false);
		if (SceneManager.GetActiveScene ().name == "gym") {
			
			if (other.gameObject.name.StartsWith ("Wall1")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("Treadmill") ||
			           other.gameObject.name.StartsWith ("GymMember") ||
			           other.gameObject.name.StartsWith ("Phone")) {
				if (ScoreManager.energy - 3 < 0 || ScoreManager.rest - 1 < 0) {
					exitScene ();
				}
				ScoreManager.muscle += 2;
				ScoreManager.energy -= 3;
				ScoreManager.rest -= 1;

			} else if (other.gameObject.name.StartsWith ("Dumbell") ||
			           other.gameObject.name.StartsWith ("Power-up") ||
			           other.gameObject.name.StartsWith ("boombox")) {
				if (ScoreManager.energy - 2 < 0 || ScoreManager.rest - 1 < 0) {
					exitScene ();
				}
				ScoreManager.muscle += 5;
				ScoreManager.energy -= 2;
				ScoreManager.rest -= 1;
			}
		} else if (SceneManager.GetActiveScene ().name == "store") {
			if (other.gameObject.name.StartsWith ("Wall2")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("chips") ||
			           other.gameObject.name.StartsWith ("donut")) {
				if (ScoreManager.money - 3 < 0 || ScoreManager.rest - 1 < 0) {
					exitScene ();
				}
				ScoreManager.energy += 2;
				ScoreManager.money -= 3;
				ScoreManager.rest -= 1;

			} else if (other.gameObject.name.StartsWith ("steak") ||
			           other.gameObject.name.StartsWith ("Power-up")) {
				if (ScoreManager.money - 2 < 0 || ScoreManager.rest - 1 < 0) {
					exitScene ();
				}
				ScoreManager.energy += 5;
				ScoreManager.money -= 2;
				ScoreManager.rest -= 1;
			}
		} else if (SceneManager.GetActiveScene ().name == "work") {
			if (other.gameObject.name.StartsWith ("Wall3")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("donut")) {
				if (ScoreManager.muscle - 3 < 0 || ScoreManager.rest - 1 < 0) {
					exitScene ();
				}
				ScoreManager.muscle -= 3;
				ScoreManager.rest -= 1;

			} else if (other.gameObject.name.StartsWith ("money")) {
				if (ScoreManager.rest - 1 < 0) {
					exitScene ();
				}
				ScoreManager.money += 5;
				ScoreManager.rest -= 1;
			}
		} else if (SceneManager.GetActiveScene ().name == "home") {
			if (other.gameObject.name.StartsWith ("Wall4")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("tv") ||
				other.gameObject.name.StartsWith ("controller")) {
				if (ScoreManager.rest - 2 < 0) {
					exitScene ();
				}
				ScoreManager.rest -= 2;

			} else if (other.gameObject.name.StartsWith ("bed")) {
				
				ScoreManager.rest += 5;
			}
		}
	}

	void exitScene(){
		SceneManager.LoadScene ("title");
	}


		/*if (other.gameObject.name.StartsWith("Bat") || other.gameObject.name.StartsWith("RockT") 
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
*/
    //Delay to allow time for animation to play before end of level
	IEnumerator delay(){

		yield return new WaitForSeconds (8f);
		//SceneManager.LoadScene ("Menu");

	}



}
