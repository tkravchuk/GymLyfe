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
	private bool updateOn = true;
	private PlayOneShotScript clip;
	public static MovementScript current;
	public int muscleSum;
	public int energySum;
	public int restSum;
	public int moneySum;

	Rigidbody2D myRigidbody;

	public float timer = 46;
	Text timerText;
	// Use this for initialization
	void Start () {
		current = this;
		myRigidbody = GetComponent<Rigidbody2D> ();
		//myRigidbody.gravityScale = gravity;
		//clip = GameObject.Find("AudioManager").GetComponent<PlayOneShotScript>();
		GameObject eScore = GameObject.Find ("timer");
		timerText = eScore.GetComponent <Text> ();

		muscleSum = 0;
		energySum = 0;
		restSum = 0;
		moneySum = 0;

		checkScores ();
		rate ();


	}


	void checkScores(){
		if (SceneManager.GetActiveScene ().name == "gym") {

				if (ScoreManager.energy + energySum - 3 <= 0 || ScoreManager.rest + restSum - 1 <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}

		} else if (SceneManager.GetActiveScene ().name == "store") {
				if (ScoreManager.money + moneySum - 3 <= 0 || ScoreManager.rest + restSum - 1 <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}

		} else if (SceneManager.GetActiveScene ().name == "work") {
				if (ScoreManager.muscle + muscleSum - 3 <= 0 || ScoreManager.rest + restSum - 1 <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}

		} else if (SceneManager.GetActiveScene ().name == "home") {
				if (ScoreManager.muscle + muscleSum - 3 <= 0 || ScoreManager.energy + energySum - 2 <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
		}
	}

	// Update is called once per frame

	void Update () {
		//stageChange = ScoreManager.stage;
		//print(AssetFireScript.current.fireTime);
		checkScores();
		rate ();



		timer -= Time.deltaTime;
		if (timer < 0) {
			ScoreManager.energy += energySum;
			ScoreManager.rest += restSum;
			ScoreManager.money += moneySum;
			ScoreManager.muscle += muscleSum;
			exitScene ();
		}
		timerText.text = "Timer: " + (int)timer;

		if (updateOn == true) { // && Input.touchCount > 0    -- include in if stat. when using mobile platform

			//var touch = Input.GetTouch(0);     			  -- again, only for mobile
			if ((Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width/2)  || Input.GetKey("left"))    //touch.position.x < Screen.width/2 || 
				{
					if (myRigidbody.transform.position.x > -xChange){
						myRigidbody.transform.position = new Vector2 (transform.position.x-.12f, transform.position.y);
					//transform.position.x -= 1;
					//DoLeftSideStuff();
					}
				}
			else if ((Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width/2) || Input.GetKey("right"))  //touch.position.x > Screen.width/2 ||
				{
					if(myRigidbody.transform.position.x < xChange){
						myRigidbody.transform.position = new Vector2 (transform.position.x+.12f, transform.position.y);
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
			AssetFireScript.current.fireTime = 1 / (ScoreManager.stage+1);
			/*if (AssetFireScript.current.fireTime >= .2) {
				AssetFireScript.current.fireTime -= .2f;
			}*/
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
			if (((ScoreManager.muscle + muscleSum) / 100) - ScoreManager.stage >= 2) {
				multiplier = (ScoreManager.muscle + muscleSum) / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall1")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("Treadmill") ||
			           other.gameObject.name.StartsWith ("GymMember") ||
			           other.gameObject.name.StartsWith ("Phone")) {
				if (ScoreManager.energy + energySum - 3 * multiplier <= 0 || ScoreManager.rest + restSum - 1 * multiplier <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
				muscleSum += 2;
				energySum -= 3 * multiplier;
				restSum -= 1 * multiplier;

			} else if (other.gameObject.name.StartsWith ("Dumbell") ||
			           other.gameObject.name.StartsWith ("Power-up") ||
			           other.gameObject.name.StartsWith ("boombox")) {
				if (ScoreManager.energy + energySum - 2 * multiplier <= 0 || ScoreManager.rest + restSum - 1 * multiplier <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
				muscleSum += 5;
				energySum -= 2 * multiplier;
				restSum -= 1 * multiplier;
			}
		} else if (SceneManager.GetActiveScene ().name == "store") {
			if (((ScoreManager.energy + energySum) / 100) - ScoreManager.stage >= 2) {
				multiplier = (ScoreManager.energy + energySum) / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall2")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("chips") ||
			           other.gameObject.name.StartsWith ("donut")) {
				if (ScoreManager.money + moneySum - 3 * multiplier <= 0 || ScoreManager.rest + restSum - 1 * multiplier <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
				energySum += 2;
				moneySum -= 3 * multiplier;
				restSum -= 1 * multiplier;

			} else if (other.gameObject.name.StartsWith ("steak") ||
			           other.gameObject.name.StartsWith ("Power-up")) {
				if (ScoreManager.money + moneySum - 2 * multiplier <= 0 || ScoreManager.rest + restSum - 1 * multiplier <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
				energySum += 5;
				moneySum -= 2 * multiplier;
				restSum -= 1 * multiplier;
			}
		} else if (SceneManager.GetActiveScene ().name == "work") {
			if (((ScoreManager.money + moneySum) / 100) - ScoreManager.stage >= 2) {
				multiplier = (ScoreManager.money + moneySum) / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall3")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("donut")) {
				if (ScoreManager.muscle - 3 * multiplier <= 0 || ScoreManager.rest - 1 * multiplier <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
				muscleSum -= 3 * multiplier;
				restSum -= 1 * multiplier;

			} else if (other.gameObject.name.StartsWith ("money")) {
				if (ScoreManager.rest - 1 * multiplier <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
				moneySum += 5;
				restSum -= 1 * multiplier;
			}
		} else if (SceneManager.GetActiveScene ().name == "home") {
			if (((ScoreManager.rest + restSum) / 100) - ScoreManager.stage >= 2) {
				multiplier = (ScoreManager.rest + restSum) / 100 - ScoreManager.stage;
			} else {
				multiplier = 1;
			}
			if (other.gameObject.name.StartsWith ("Wall4")) {

				exitScene ();
			} else if (other.gameObject.name.StartsWith ("tv") ||
				other.gameObject.name.StartsWith ("controller")) {
				if (ScoreManager.muscle - 3  * multiplier <= 0 || ScoreManager.energy - 2 * multiplier <= 0) {
					ScoreManager.energy += energySum;
					ScoreManager.rest += restSum;
					ScoreManager.money += moneySum;
					ScoreManager.muscle += muscleSum;
					exitScene ();
				}
				muscleSum -= 3 * multiplier;
				energySum -= 2 * multiplier;


			} else if (other.gameObject.name.StartsWith ("bed")) {
				
				restSum += 5;
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
