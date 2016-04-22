using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AssetPoolerScript : MonoBehaviour {

	public int count = 0;

	public static AssetPoolerScript current;
	[System.Serializable]
	public class PooledObject{
		public GameObject pooledObject;
		public int pooledAmount;
	}
	//public GameObject[] pooledObject;
	//public int pooledAmount = 20;
	public bool willGrow = true;
	public PooledObject[] poolObject;
	public int amount;
	List<GameObject>[] pooledObjects;

	void Awake(){
		current = this;
	}

	void Start () {
		pooledObjects = new List<GameObject>[poolObject.Length];

		for (int j = 0; j < poolObject.Length; j++) {
			pooledObjects [j] = new List<GameObject> ();
			for (int i = 0; i < poolObject[j].pooledAmount; i++) {
				GameObject obj = (GameObject)Instantiate (poolObject[j].pooledObject);
				setSpeed (rate (), obj);
				obj.SetActive (false);
				pooledObjects[j].Add (obj);
			}
		}
		amount = poolObject.Length;
	}


	public GameObject GetPooledObject() {
		//for (int i = 0; i < poolObject.Length; i++) {
			var iIndex = Random.Range (0, (poolObject.Length-1));

		for (int j = 0; j < pooledObjects [iIndex].Count; j++) {
				//var jIndex = Random.Range (0, (pooledObjects [iIndex].Count));
			if (!pooledObjects [iIndex] [j].activeInHierarchy) {
				return pooledObjects [iIndex] [j];
			}


			}
		
			if (willGrow) {
				GameObject obj = (GameObject)Instantiate (poolObject[iIndex].pooledObject);
				pooledObjects[iIndex].Add (obj);
				return obj;
			}
		//}
	
		return null;
	}

	public void setSpeed(float rate, GameObject obj){
		/*for(int i = 0; i < pooledObjects.; i++){
			for (int j = 0; i < pooledObjects [i].Count; j++) {
				if (pooledObjects [i] [j].activeInHierarchy) {

					AssetMovementScript a = pooledObjects [i] [j].GetComponent<AssetMovementScript> ();//GameObject.GetComponents<> (AssetMovementScript.AssetSpeed) = rate;
					a.AssetSpeed = rate;
				}
			}
		}*/
		AssetMovementScript a = obj.GetComponent<AssetMovementScript> ();//GameObject.GetComponents<> (AssetMovementScript.AssetSpeed) = rate;
		a.AssetSpeed = rate;
	}

	public float rate(){
		return 3f + 1.5f * ScoreManager.stage;
	}
				

	public GameObject GetPooledObject(int iIndex) {
		//for (int i = 0; i < poolObject.Length; i++) {
		//var iIndex = Random.Range (0, (poolObject.Length));

		//for (int j = 0; j < pooledObjects [iIndex].Count; j++) {
		//var jIndex = Random.Range (0, (pooledObjects [iIndex].Count-1));
		for (int j = 0; j < pooledObjects [iIndex].Count; j++) {
			//var jIndex = Random.Range (0, (pooledObjects [iIndex].Count));
			if (!pooledObjects [iIndex] [j].activeInHierarchy) {
				return pooledObjects [iIndex] [j];
			}


		}

		//}

		if (willGrow) {
			GameObject obj = (GameObject)Instantiate (poolObject[iIndex].pooledObject);
			pooledObjects[iIndex].Add (obj);
			return obj;
		}
		//}

		return null;
	}
	


		
}
