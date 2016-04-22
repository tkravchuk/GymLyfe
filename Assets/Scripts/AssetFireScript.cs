using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class AssetFireScript : MonoBehaviour {
	public int count = 0;
	public float fireTime = 1f;
	public static AssetFireScript current;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Fire", fireTime, fireTime);
		current = this;
	}

	void Fire(){
		GameObject obj;
		//GameObject pool = GameObject.name.StartsWith("Pool");

		if (count < 3) {
			obj = AssetPoolerScript.current.GetPooledObject ();
			count++;
		} else {
			obj = AssetPoolerScript.current.GetPooledObject (AssetPoolerScript.current.amount - 1); //change this num to last number of pooled objects for wall
			count = 0;
		}


		if(obj == null) return;
		float xpos;
		if (obj.tag == "Wall") {
			xpos = Random.value * 5f + 5.5f; //5.5 -> 10.5
		} else {
			xpos = Random.value * 8.8f - 4.4f;  //-4.4 -> 4.4
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
