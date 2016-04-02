using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetFireScript : MonoBehaviour {

	public float fireTime = .5f;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Fire", fireTime, fireTime);
	}

	void Fire(){
		GameObject obj = AssetPoolerScript.current.GetPooledObject ();

		if(obj == null) return;
		float xpos;
		if (obj.tag == "Wall") {
			xpos = Random.value * 6.5f + 5.5f; //5.5 -> 12
		} else {
			xpos = Random.value * 9.0f - 4.5f;
		}
		obj.transform.position = transform.position;
		obj.transform.position = new Vector2 (xpos, transform.position.y);;
		obj.SetActive (true);
	}
	// Update is called once per frame
	/*void Update () {
		Vector3 newPosition = transform.position;

		//Make sure you scale by Time.deltaTime when doing animation. If you don't, you program will run at different speeds depending on 
		// the speed of the underlying computer.
		newPosition.y -= 2 * Time.deltaTime;
		transform.position = newPosition;
	}*/

}
