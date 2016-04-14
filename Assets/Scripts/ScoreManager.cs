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
	//Text topScore;

	private bool setOriginal;

	void Awake ()
	{
		SetScores ();
		//Set up the reference.
		GameObject eScore = GameObject.Find ("energyScore");
		energyScore = eScore.GetComponent <Text> ();
	
		GameObject rScore = GameObject.Find ("restScore");
		restScore = rScore.GetComponent <Text> ();
		GameObject musScore = GameObject.Find ("muscleScore");
		muscleScore = musScore.GetComponent <Text> ();
		GameObject monScore = GameObject.Find ("moneyScore");
		moneyScore = monScore.GetComponent <Text> ();
		if (SceneManager.GetActiveScene ().name == "title") {
			
			GameObject sScore = GameObject.Find ("stageScore");
			stageScore = sScore.GetComponent <Text> ();
		}
		/*if (SceneManager.GetActiveScene ().name == "ShapeWars") {
			score = 0;
		} else if (SceneManager.GetActiveScene ().name == "Menu") {
			GameObject best = GameObject.Find ("Best");
			topScore = best.GetComponent <Text> ();
		}
		if (topS < score) {
			topS = score;
		} */


	}

	void SetScores () 
	{
		if (setOriginal == true){
			stage = 1;
			muscle = 100;
			rest = 100;
			money = 100;
			energy = 100;
			setOriginal = false;
		}
	}

	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		//gameScore.text = "Score: " + score;


		if (SceneManager.GetActiveScene ().name == "gym" ||
			SceneManager.GetActiveScene ().name == "work" ||
			SceneManager.GetActiveScene ().name == "store" ||
			SceneManager.GetActiveScene ().name == "home") {
			energyScore.text = "Energy: \n" + energy;
			muscleScore.text = "Muscle: \n" + muscle;
			moneyScore.text = "Money: \n" + money;
			restScore.text = "Rest: \n" + rest;
		} else if (SceneManager.GetActiveScene ().name == "title") {
			energyScore.text = " Energy Score: " + energy;
			muscleScore.text = " Muscle Score: " + muscle;
			moneyScore.text = " Money Score: " + money;
			restScore.text = " Rest Score: " + rest;
			stageScore.text = " Stage: " + stage;
		}
	}
}