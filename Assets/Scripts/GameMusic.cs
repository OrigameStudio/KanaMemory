
using UnityEngine;
using System.Collections.Generic;


public class GameMusic : MonoBehaviour{

	public List<AudioClip> tracks;

	private int nextTrack;

	void Start(){

		this.Reset();
	}

	void Update(){

		if(!this.GetComponent<AudioSource>().isPlaying){

			this.GetComponent<AudioSource>().clip = this.tracks[this.nextTrack];

			this.GetComponent<AudioSource>().Play();

			nextTrack++;

			if(nextTrack >= this.tracks.Count){

				this.Reset();
			}
		}
	}

	private void Reset(){

		this.nextTrack = 0;
		this.Shuffle();
	}

	private void Shuffle(){

		int index1 = this.tracks.Count;

		while(index1 > 1){

			AudioClip value;
			int index2;

			index1--;
			index2 = Random.Range(0, index1 + 1);

			value				= this.tracks[index2];
			this.tracks[index2]	= this.tracks[index1];
			this.tracks[index1]	= value;
    	}
	}

}
