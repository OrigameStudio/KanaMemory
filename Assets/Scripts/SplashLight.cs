
using UnityEngine;
using System.Collections;


public class SplashLight : MonoBehaviour{

	void Start(){

		this.GetComponent<Light>().intensity = 0;
	}

	public void FadeIn(){

		if(this.GetComponent<Animation>().isPlaying){

			this.GetComponent<Animation>().Stop();
		}

		this.GetComponent<Animation>().clip = GetComponent<Animation>().GetClip("Light@FadeIn");

		this.GetComponent<Animation>().Rewind();

		this.GetComponent<Animation>().Play();
	}

	public void FadeOut(){

		if(this.GetComponent<Animation>().isPlaying){

			this.GetComponent<Animation>().Stop();
		}

		this.GetComponent<Animation>().clip = GetComponent<Animation>().GetClip("Light@FadeOut");

		this.GetComponent<Animation>().Rewind();

		this.GetComponent<Animation>().Play();
	}

}
