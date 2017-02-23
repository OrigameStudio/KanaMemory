using UnityEngine;
using System.Collections;

public class LingerAudio : MonoBehaviour{

	private bool played;

	void Update(){

		if(this.played){

			if(!this.GetComponent<AudioSource>().isPlaying){

				GameObject.Destroy(this.gameObject);
			}

		}else if(this.GetComponent<AudioSource>().isPlaying){

			this.played = true;

			GameObject.DontDestroyOnLoad(this.gameObject);
		}
	}
}
