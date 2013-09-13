using UnityEngine;
using System.Collections;

public class LingerAudio : MonoBehaviour{

	private bool played;

	void Start(){

		GameObject.DontDestroyOnLoad(this.gameObject);
	}

	void Update(){

		if(this.played){

			if(!this.audio.isPlaying){

				GameObject.Destroy(this.gameObject);
			}

		}else{

			this.played = this.audio.isPlaying;
		}
	}
}
