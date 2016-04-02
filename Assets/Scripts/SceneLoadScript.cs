using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadScript : MonoBehaviour {

	public string levelToLoad;

	public void LoadLevel()
	{
		SceneManager.LoadScene (levelToLoad);
	}

	// Use this for initialization
	void Start () {
		/*
		if (PlayerPrefs.HasKey (levelToLoad) && PlayerPrefs.GetInt (levelToLoad) == 1) {
			this.gameObject.SetActive(true);
		} else {
			this.gameObject.SetActive(false);
		}
		*/


	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "Menu" && Input.GetButtonDown ("Jump")) {
			SceneManager.LoadScene ("ShapeWars");
		}
	}
}
