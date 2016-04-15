using UnityEngine;
using System.Collections;

public class AssetMovementScript : MonoBehaviour {

	// Use this for initialization
	public float AssetSpeed = 3;
	public static AssetMovementScript current;
	void Start () {
		/*GameObject obj = AssetPoolerScript.current.GetPooledObject ();

		if(obj == null) return;

		obj.transform.position = gameObject.transform.position;
		obj.SetActive (true);*/
		//current = this;
	}
	void Awake(){
		current = this;
	}
	
	void Update () {
		Vector3 newPosition = transform.position;

		//Make sure you scale by Time.deltaTime when doing animation. If you don't, you program will run at different speeds depending on 
		// the speed of the underlying computer.
		newPosition.y -= AssetSpeed * Time.deltaTime;
		gameObject.transform.position = newPosition;

		if (newPosition.y < -8) { // Bad! Hardcoded the edge of the screen. May want to think about either making this a parameter, a constant, or derive it from the scene
			gameObject.SetActive (false);

		}
	}


}
