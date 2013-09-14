using UnityEngine;
using System.Collections;

public class LingerAudio : MonoBehaviour{

	private bool played;

	void Update(){

		if(this.played){

			if(!this.audio.isPlaying){

				GameObject.Destroy(this.gameObject);
			}

		}else if(this.audio.isPlaying){

			this.played = true;

			GameObject.DontDestroyOnLoad(this.gameObject);
		}
	}
}
