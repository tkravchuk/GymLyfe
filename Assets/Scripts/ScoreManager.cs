//Taras Kravchuk
//CPSC 466 Game Programming
//CaveRunner
//Script for managing the player score in level and saving personal best score

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
	public static int stage {
		get {
			return PlayerPrefs.GetInt ("stage");
		}
		set{
			PlayerPrefs.SetInt ("stage", value);
		}
	}// The player's score.
	public static int rest {
		get {
			return PlayerPrefs.GetInt ("rest");
		}
		set{
			PlayerPrefs.SetInt ("rest", value);
		}
	}

	public static int energy {
		get {
			return PlayerPrefs.GetInt ("energy");
		}
		set{
			PlayerPrefs.SetInt ("energy", value);
		}
	}
	public static int muscle {
		get {
			return PlayerPrefs.GetInt ("muscle");
		}
		set{
			PlayerPrefs.SetInt ("muscle", value);
		}
	}
	public static int money {
		get {
			return PlayerPrefs.GetInt ("money");
		}
		set{
			PlayerPrefs.SetInt ("money", value);
		}
	}

	Text energyScore;
	Text muscleScore;
	Text moneyScore;
	Text restScore;

	Text stageScore;                      // Reference to the Text component.
	Text topScore;

	private bool setOriginal = false;

	void Awake ()
	{
		// Set up the reference.
		GameObject game = GameObject.Find ("Score");
		gameScore = game.GetComponent <Text> ();
		if (SceneManager.GetActiveScene ().name == "ShapeWars") {
			score = 0;
		} else if (SceneManager.GetActiveScene ().name == "Menu") {
			GameObject best = GameObject.Find ("Best");
			topScore = best.GetComponent <Text> ();
		}
		if (topS < score) {
			topS = score;
		} 


	}

	void SetScores () 
	{
		if (setOriginal == false){
			stage = 1;
			muscle = 100;
			rest = 100;
			money = 100;
			energy = 100;
			setOriginal = true;
		}
	}

	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		gameScore.text = "Score: " + score;
		if (SceneManager.GetActiveScene ().name == "Menu") {
			topScore.text = "Personal Best: " + topS;
		}
	}
}