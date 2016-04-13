using UnityEngine;
using System.Collections;

public class BatScript : MonoBehaviour {
	/*
	//The arrow's horizontal speed - Can change this in the Inspector
	public float hSpeed = 3;

	//Where the arrow starts from so we can respawn a new arrow near that location.
	Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = gameObject.transform.position;
		//Speed = 1;
	}
	void Awake (){
	}

	// Update is called once per frame
	void Update () {
		Vector3 newPosition = gameObject.transform.position;

		//Make sure you scale by Time.deltaTime when doing animation. If you don't, you program will run at different speeds depending on 
		// the speed of the underlying computer.
		newPosition.x -= hSpeed * rate() * Time.deltaTime * GameManager.Speed;
		gameObject.transform.position = newPosition;
		if (newPosition.x < -11) { // Bad! Hardcoded the edge of the screen. May want to think about either making this a parameter, a constant, or derive it from the scene
			MakeNewBat ();
			Destroy (gameObject);
		}



	}
	float rate() {
		if (ScoreManager.score < 20) {
			return 1f;
		} else if (ScoreManager.score < 50) {
			return 1.3f;
		} else if (ScoreManager.score < 100) {
			return 2f;
		} else {
			return 3f;
		}			
	}

	void MakeNewBat() {
		//Another hardcoded range. Goes between -3 and +3 on the y-axis when the arrow respawns
		startingPosition.y = Random.value * 2.7f + 1f;
		Object newBat = Instantiate (this.gameObject, startingPosition, Quaternion.identity);
		//The name gets a little crazy if you don't do this. Like: arrow(Clone)(Clone)(Clone)(CLone)...
		newBat.name = "Bat";
	}
	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.name.StartsWith("Runner")){
			//hSpeed = 0;
			Destroy(gameObject);
		}
	}*/
}
