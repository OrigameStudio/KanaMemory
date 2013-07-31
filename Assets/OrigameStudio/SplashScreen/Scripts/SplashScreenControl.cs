using UnityEngine;
using System.Collections;

public enum SplashScreenStatus{

	fadingIn	= 0,
	ready		= 1,
	fadingOut	= 2,
	finished	= 3
};

public class SplashScreenControl : MonoBehaviour{

	private	SplashScreenStatus			status = SplashScreenStatus.fadingIn;
	public	float						explosionForce;
	public	float						explosionRadius;
	public	SplashScreenCollapsable[]	collapsableObjects;
	public	SplashScreenLight[]			lights;
	public	int							nextScene = -1;
	public	Color						ambientLight = Color.black;


	void Start(){

		RenderSettings.ambientLight = this.ambientLight;

		this.animation.Play();
	}

	void Update(){

		if( Input.GetMouseButtonDown(0) ){

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if( Physics.Raycast(ray, out hit) ){

				if( hit.collider.gameObject.GetComponent<SplashScreenCollapsable>() != null ){

					this.Skip(hit.point);
				}
			}
		}
	}

	public void Skip(Vector3? hitPoint){

		if(this.status == SplashScreenStatus.ready || this.status == SplashScreenStatus.fadingOut){

			this.Collapse(hitPoint);
		}

		if(this.status == SplashScreenStatus.ready){

			if(this.animation.isPlaying){

				this.animation.Stop();
			}

			this.animation.clip = this.animation.GetClip("SplashScreen@Skip");

			this.animation.Rewind();

			this.animation.Play();
		}
	}

	public void Collapse(Vector3? hitPoint){

		foreach(SplashScreenCollapsable collapsableObject in this.collapsableObjects){

			collapsableObject.Collapse(hitPoint, explosionForce, explosionRadius);
		}
	}

	public void FadeIn(){

		this.status = SplashScreenStatus.fadingIn;

		foreach(SplashScreenLight light in lights){

			light.FadeIn();
		}
	}

	public void FadeOut(){

		this.status = SplashScreenStatus.fadingOut;

		foreach(SplashScreenLight light in lights){

			light.FadeOut();
		}
	}

	public void Ready(){

		this.status = SplashScreenStatus.ready;

		this.audio.Play();
	}

	private void Finished(){

		this.status = SplashScreenStatus.finished;

		Application.LoadLevel(this.nextScene);
	}

}
