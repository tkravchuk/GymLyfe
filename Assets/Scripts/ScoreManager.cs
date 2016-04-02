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
	public static int score {
		get {
			return PlayerPrefs.GetInt ("score");
		}
		set{
			PlayerPrefs.SetInt ("score", value);
		}
	}// The player's score.
	public static int topS {
		get {
			return PlayerPrefs.GetInt ("topS");
		}
		set{
			PlayerPrefs.SetInt ("topS", value);
		}
	}


	Text gameScore;                      // Reference to the Text component.
	Text topScore;

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


	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		gameScore.text = "Score: " + score;
		if (SceneManager.GetActiveScene ().name == "Menu") {
			topScore.text = "Personal Best: " + topS;
		}
	}
}