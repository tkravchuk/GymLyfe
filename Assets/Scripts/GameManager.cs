//Taras Kravchuk
//CPSC 466 Game Programming
//CaveRunner

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static float Speed {
		get {
			return PlayerPrefs.GetFloat ("Speed");
		}
		set{
			PlayerPrefs.SetFloat ("Speed", value);
		}
	}
		

	void Awake ()
	{
		Speed = 1;
	}
}
