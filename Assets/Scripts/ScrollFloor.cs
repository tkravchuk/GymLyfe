//Taras Kravchuk
//CPSC 466 Game Programming
//CaveRunner
//Script for scrolling the background

using UnityEngine;
using System.Collections;

public class ScrollFloor : MonoBehaviour
{
	public float scrollSpeed;
	public static ScrollFloor current;

	void Start ()
	{
		current = this;
	}

	void Update ()
	{
		Vector2 offset = new Vector2 (0, Time.time * scrollSpeed);
		GetComponent<Renderer>().material.mainTextureOffset = offset;


	}


}
