using UnityEngine;
using System.Collections;

public class PlayOneShotScript : MonoBehaviour {

	public AudioClip[] soundToPlay;

	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		audioSource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	public void SetVolume(float n){
		audioSource.volume = n;
	}
	public void PlayOneShot(int n) {
		audioSource.PlayOneShot (soundToPlay[n]);
	}
/*	public void Play() {
		audioSource.Play (soundToPlay[3]);
	}*/
}
