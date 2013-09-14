
using UnityEngine;
using System.Collections;


public class SplashLight : MonoBehaviour{

	void Start(){

		this.light.intensity = 0;
	}

	public void FadeIn(){

		if(this.animation.isPlaying){

			this.animation.Stop();
		}

		this.animation.clip = animation.GetClip("Light@FadeIn");

		this.animation.Rewind();

		this.animation.Play();
	}

	public void FadeOut(){

		if(this.animation.isPlaying){

			this.animation.Stop();
		}

		this.animation.clip = animation.GetClip("Light@FadeOut");

		this.animation.Rewind();

		this.animation.Play();
	}

}
