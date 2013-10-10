
using UnityEngine;
using System.Collections;


public enum SplashStatus{

	fadingIn	= 0,
	ready		= 1,
	fadingOut	= 2,
	finished	= 3
};

public class SplashControl : MonoBehaviour{

	private	SplashStatus			status = SplashStatus.fadingIn;
	public	float					explosionForce;
	public	float					explosionRadius;
	public	SplashCollapsable[]		collapsableObjects;
	public	SplashLight[]			lights;
	public	int						nextScene = -1;
	public	Color					ambientLight = Color.black;
	public	string					skipAnimationClip;
	public	bool					escapeExitsApplication;
	public	bool					showActivityIndicator = false;

#if UNITY_ANDROID || UNITY_EDITOR

	public AndroidActivityIndicatorStyle androidActivityIndicator = AndroidActivityIndicatorStyle.DontShow;

#endif

#if UNITY_IPHONE || UNITY_EDITOR

	public iOSActivityIndicatorStyle iOsActivityIndicator = iOSActivityIndicatorStyle.DontShow;

#endif

	protected	bool				exitApplication = false;

	void Start(){

		RenderSettings.ambientLight = this.ambientLight;

		this.animation.Play();
	}

	void Update(){

		if( this.status == SplashStatus.ready && Input.GetMouseButtonDown(0) ){

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if( Physics.Raycast(ray, out hit) ){

				SplashCollapsable collapsable = hit.collider.gameObject.GetComponent<SplashCollapsable>();

				if(collapsable != null ){

					collapsable.Hit(hit.point);

					if(collapsable.exitApplication){

						this.exitApplication = true;

					}else if(collapsable.nextScene >= 0){

						this.nextScene = collapsable.nextScene;
					}

					this.Skip(hit.point);
				}
			}
		}

		if( Input.GetKey(KeyCode.Escape) ){

			if(this.escapeExitsApplication){

				this.exitApplication = true;
			}

			this.Skip(null);
		}
	}

	public IEnumerator StartActivityIndicator(){

#if UNITY_ANDROID

		Handheld.SetActivityIndicatorStyle(this.androidActivityIndicator);
		Handheld.StartActivityIndicator();

#elif UNITY_IPHONE

		Handheld.SetActivityIndicatorStyle(this.iOsActivityIndicator);
		Handheld.StartActivityIndicator();

#endif

		yield return new WaitForSeconds(0);
	}

	public void Skip(Vector3? hitPoint){

		if(this.status == SplashStatus.ready || this.status == SplashStatus.fadingOut){

			this.Collapse(hitPoint);
		}

		if(this.status == SplashStatus.ready){

			if(this.animation.isPlaying){

				this.animation.Stop();
			}

			this.animation.clip = this.animation.GetClip(this.skipAnimationClip);

			this.animation.Rewind();

			this.animation.Play();
		}
	}

	public void Collapse(Vector3? hitPoint){

		foreach(SplashCollapsable collapsableObject in this.collapsableObjects){

			collapsableObject.Collapse(hitPoint, explosionForce, explosionRadius);
		}
	}

	public void FadeIn(){

		this.status = SplashStatus.fadingIn;

		foreach(SplashLight light in lights){

			light.FadeIn();
		}
	}

	public void FadeOut(){

		this.status = SplashStatus.fadingOut;

		foreach(SplashLight light in lights){

			light.FadeOut();
		}
	}

	public void Ready(){

		this.status = SplashStatus.ready;

		this.audio.Play();
	}

	private void Finished(){

		this.status = SplashStatus.finished;

		if(this.exitApplication){

			Debug.Log("Bye! ^_^");

			Application.Quit();

		}else{

			if(this.showActivityIndicator){

				this.StartCoroutine( this.StartActivityIndicator() );
			}

			Application.LoadLevel(this.nextScene);
		}
	}

}
