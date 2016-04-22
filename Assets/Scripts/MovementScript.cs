//Taras Kravchuk
//CPSC 466 Game Programming
//CaveRunner
//Script for managing the players movement
using System;
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
	public float timer = 46;
	Text timerText;
	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		//myRigidbody.gravityScale = gravity;
		//clip = GameObject.Find("AudioManager").GetComponent<PlayOneShotScript>();
		GameObject eScore = GameObject.Find ("timer");
		timerText = eScore.GetComponent <Text> ();

		checkScores ();
		rate ();
	}


	void checkScores(){
		if (SceneManager.GetActiveScene ().name == "gym") {

				if (ScoreManager.energy - 3 <= 0 || ScoreManager.rest - 1 <= 0) {
					exitScene ();
				}

		} else if (SceneManager.GetActiveScene ().name == "store") {
				if (ScoreManager.money - 3 <= 0 || ScoreManager.rest - 1 <= 0) {
					exitScene ();
				}

		} else if (SceneManager.GetActiveScene ().name == "work") {
				if (ScoreManager.muscle - 3 <= 0 || ScoreManager.rest - 1 <= 0) {
					exitScene ();
				}

		} else if (SceneManager.GetActiveScene ().name == "home") {
				if (ScoreManager.muscle - 3 <= 0 || ScoreManager.energy - 2 <= 0) {
					exitScene ();
				}

		}
	}

	// Update is called once per frame

	void Update () {
		//stageChange = ScoreManager.stage;
		print(AssetFireScript.current.fireTime);
		checkScores();
		rate ();



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
		ScoreManager.stage = Math.Max (Math.Min (Math.Min (ScoreManager.muscle / 100, ScoreManager.energy / 100), 
			Math.Min (ScoreManager.rest / 100, ScoreManager.money / 100)), ScoreManager.stage);
		ScrollFloor.current.scrollSpeed = ScoreManager.stage * 1.5f + 3f;
		//AssetFireScript.current.fireTime = .5f;
		if (stageChange != ScoreManager.stage) {
			stageChange = ScoreManager.stage;
			//rate ();
			ScrollFloor.current.scrollSpeed = ScoreManager.stage * 1.5f + 3f;
			if (AssetFireScript.current.fireTime >= .2) {
				AssetFireScript.current.fireTime -= .2f;
			}
			//onGUI ();
			//StartCoroutine(delay());
			exitScene ();
		}

	}

	//Called on collision
	void OnCollisionEnter2D(Collision2D other) {
		int multiplier = 1;
		other.gameObject.SetActive (false);
		if (SceneManager.GetActiveScene ().name == "gym") {
			if ((ScoreManager.muscle / 100) - ScoreManager.stage >= 2) {
				multiplier = ScoreManager.muscle / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall1")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("Treadmill") ||
			           other.gameObject.name.StartsWith ("GymMember") ||
			           other.gameObject.name.StartsWith ("Phone")) {
				if (ScoreManager.energy - 3 * multiplier <= 0 || ScoreManager.rest - 1 * multiplier <= 0) {
					exitScene ();
				}
				ScoreManager.muscle += 2;
				ScoreManager.energy -= 3 * multiplier;
				ScoreManager.rest -= 1 * multiplier;

			} else if (other.gameObject.name.StartsWith ("Dumbell") ||
			           other.gameObject.name.StartsWith ("Power-up") ||
			           other.gameObject.name.StartsWith ("boombox")) {
				if (ScoreManager.energy - 2 * multiplier <= 0 || ScoreManager.rest - 1 * multiplier <= 0) {
					exitScene ();
				}
				ScoreManager.muscle += 5;
				ScoreManager.energy -= 2 * multiplier;
				ScoreManager.rest -= 1 * multiplier;
			}
		} else if (SceneManager.GetActiveScene ().name == "store") {
			if ((ScoreManager.energy / 100) - ScoreManager.stage >= 2) {
				multiplier = ScoreManager.muscle / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall2")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("chips") ||
			           other.gameObject.name.StartsWith ("donut")) {
				if (ScoreManager.money - 3 * multiplier <= 0 || ScoreManager.rest - 1 * multiplier <= 0) {
					exitScene ();
				}
				ScoreManager.energy += 2;
				ScoreManager.money -= 3 * multiplier;
				ScoreManager.rest -= 1 * multiplier;

			} else if (other.gameObject.name.StartsWith ("steak") ||
			           other.gameObject.name.StartsWith ("Power-up")) {
				if (ScoreManager.money - 2 * multiplier <= 0 || ScoreManager.rest - 1 * multiplier <= 0) {
					exitScene ();
				}
				ScoreManager.energy += 5;
				ScoreManager.money -= 2 * multiplier;
				ScoreManager.rest -= 1 * multiplier;
			}
		} else if (SceneManager.GetActiveScene ().name == "work") {
			if ((ScoreManager.money / 100) - ScoreManager.stage >= 2) {
				multiplier = ScoreManager.muscle / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall3")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("donut")) {
				if (ScoreManager.muscle - 3 * multiplier <= 0 || ScoreManager.rest - 1 * multiplier <= 0) {
					exitScene ();
				}
				ScoreManager.muscle -= 3 * multiplier;
				ScoreManager.rest -= 1 * multiplier;

			} else if (other.gameObject.name.StartsWith ("money")) {
				if (ScoreManager.rest - 1 * multiplier <= 0) {
					exitScene ();
				}
				ScoreManager.money += 5;
				ScoreManager.rest -= 1 * multiplier;
			}
		} else if (SceneManager.GetActiveScene ().name == "home") {
			if ((ScoreManager.rest / 100) - ScoreManager.stage >= 2) {
				multiplier = ScoreManager.muscle / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall4")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("tv") ||
				other.gameObject.name.StartsWith ("controller")) {
				if (ScoreManager.muscle - 3  * multiplier <= 0 || ScoreManager.energy - 2 * multiplier <= 0) {
					exitScene ();
				}
				ScoreManager.muscle -= 3 * multiplier;
				ScoreManager.energy -= 2 * multiplier;


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
