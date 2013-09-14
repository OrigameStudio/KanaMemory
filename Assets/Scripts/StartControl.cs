
using UnityEngine;
using System.Collections;


public enum StartStatus{

	fadingIn	= 0,
	ready		= 1,
	fadingOut	= 2,
	finished	= 3
};


public class StartControl : MonoBehaviour{
	
	public MemoryGame game;
	
	private	StartStatus				status = StartStatus.fadingIn;
	public	float					explosionForce = 1000f;
	public	float					explosionRadius = 0.1f;
	public	StartButton[]			buttons;
	public	SplashLight[]			lights;
	public	int						homeScene = 1;
	public	int						gameScene = 4;
	public	bool					startGame = false;
	public	Color					ambientLight = Color.black;
	public	string					skipAnimationClip;
	public	AudioSource				sound;
	
	void Start(){
		
		if(this.game == null){

			this.game = MemoryGame.GetInstance();

			if(this.game == null){

				Debug.LogError("Cannot find MemoryGame instance!");
				Application.LoadLevel(this.homeScene);
			}
		}
		
		RenderSettings.ambientLight = this.ambientLight;

		this.animation.Play();
	}
	
	public void SelectGameType(GameType gameType){
		
		this.game.type = gameType;
	}

	void Update(){

		if( !this.startGame && Input.GetMouseButtonDown(0) ){

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if( Physics.Raycast(ray, out hit) ){

				StartButton button = hit.collider.gameObject.GetComponent<StartButton>();

				if(button != null){
					
					this.SelectGameType(button.gameType);
					
					this.startGame = true;

					foreach(StartButton tmp in this.buttons){
			
						tmp.rigidbody.isKinematic = false;
					}

					button.rigidbody.useGravity = true;
					button.rigidbody.AddExplosionForce(explosionForce, hit.point, explosionRadius);
					
					this.sound.Play();
					
					this.Skip();
				}
			}
			
		}else if( Input.GetKey(KeyCode.Escape) ){

			this.Skip();
		}
	}

	public void Skip(){

		if(this.status == StartStatus.ready){

			if(this.animation.isPlaying){

				this.animation.Stop();
			}

			this.animation.clip = this.animation.GetClip(this.skipAnimationClip);

			this.animation.Rewind();

			this.animation.Play();
		}
	}

	public void FadeIn(){

		this.status = StartStatus.fadingIn;

		foreach(SplashLight light in lights){

			light.FadeIn();
		}
	}

	public void FadeOut(){

		this.status = StartStatus.fadingOut;

		foreach(SplashLight light in lights){

			light.FadeOut();
		}
	}

	public void Ready(){

		this.status = StartStatus.ready;

		this.audio.Play();
	}

	private void Finished(){

		this.status = StartStatus.finished;
		
		if(this.startGame){
			
			Application.LoadLevel(this.gameScene);
			
		}else{
			
			Application.LoadLevel(this.homeScene);
		}
	}

}
