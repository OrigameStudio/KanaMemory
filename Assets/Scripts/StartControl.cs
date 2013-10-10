
using UnityEngine;
using System.Collections;


public enum StartStatus{

	fadingIn	= 0,
	ready		= 1,
	fadingOut	= 2,
	finished	= 3
};


public class StartControl : MonoBehaviour{

	public	StartI18n				i18n;

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

	public	GameObject				difficultyEasy;
	public	GameObject				difficultyMedium;
	public	GameObject				difficultyHard;

	public	GameObject				sizeSmall;
	public	GameObject				sizeRegular;
	public	GameObject				sizeBig;

	public	bool					showActivityIndicator = false;

#if UNITY_ANDROID || UNITY_EDITOR

	public AndroidActivityIndicatorStyle androidActivityIndicator = AndroidActivityIndicatorStyle.DontShow;

#endif

#if UNITY_IPHONE || UNITY_EDITOR

	public iOSActivityIndicatorStyle iOsActivityIndicator = iOSActivityIndicatorStyle.DontShow;

#endif

	private MemoryGame game;

	void Start(){

		if(this.game == null){

			this.game = MemoryGame.GetInstance();

			if(this.game == null){

				Debug.LogError("Cannot find MemoryGame instance!");
				Application.LoadLevel(this.homeScene);
			}
		}

		this.i18n.UpdateLanguage(this.game);
		this.ShowDifficultyLevel();
		this.ShowBoardSize();

		RenderSettings.ambientLight = this.ambientLight;

		this.animation.Play();
	}


	void Update(){

		if( this.status == StartStatus.ready && !this.startGame && Input.GetMouseButtonDown(0) ){

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if( Physics.Raycast(ray, out hit) ){

				StartButton button = hit.collider.gameObject.GetComponent<StartButton>();

				if(button != null){

					this.OnAction(button.action, hit.point);
				}
			}

		}else if( Input.GetKey(KeyCode.Escape) ){

			this.Skip();
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

			if(this.showActivityIndicator){

				this.StartCoroutine( this.StartActivityIndicator() );
			}

			Application.LoadLevel(this.gameScene);

		}else{

			Application.LoadLevel(this.homeScene);
		}
	}

	public void SelectGameType(StartHUDAction action, Vector3? hitPoint){

		StartButton clickedButton = null;

		this.game.type = this.parseAction(action);

		this.startGame = true;

		foreach(StartButton button in this.buttons){

			button.rigidbody.isKinematic = false;

			if(button.action == action){

				clickedButton = button;
			}
		}

		if(clickedButton != null){

			clickedButton.rigidbody.useGravity = true;
			clickedButton.rigidbody.AddExplosionForce(explosionForce, hitPoint.Value, explosionRadius);
		}

		this.sound.Play();

		this.Skip();
	}

	public void SwitchDifficultyLevel(){

		switch(this.game.difficulty){

			case GameDifficulty.EASY:

				this.game.difficulty = GameDifficulty.MEDIUM;
				break;

			case GameDifficulty.MEDIUM:

				this.game.difficulty = GameDifficulty.HARD;
				break;

			case GameDifficulty.HARD:

				this.game.difficulty = GameDifficulty.EASY;
				break;
		}

		this.ShowDifficultyLevel();
		this.i18n.UpdateLanguage(this.game);
	}

	public void SwitchBoardSize(){

		switch(this.game.boardSize){

			case BoardSize.SMALL:

				this.game.boardSize = BoardSize.REGULAR;
				break;

			case BoardSize.REGULAR:

				this.game.boardSize = BoardSize.BIG;
				break;

			case BoardSize.BIG:

				this.game.boardSize = BoardSize.SMALL;
				break;
		}

		this.ShowBoardSize();
		this.i18n.UpdateLanguage(this.game);
	}

	public void ShowDifficultyLevel(){

		this.difficultyEasy.SetActive(this.game.difficulty == GameDifficulty.EASY);
		this.difficultyMedium.SetActive(this.game.difficulty == GameDifficulty.MEDIUM);
		this.difficultyHard.SetActive(this.game.difficulty == GameDifficulty.HARD);
	}

	public void ShowBoardSize(){

		this.sizeSmall.SetActive(this.game.boardSize == BoardSize.SMALL);
		this.sizeRegular.SetActive(this.game.boardSize == BoardSize.REGULAR);
		this.sizeBig.SetActive(this.game.boardSize == BoardSize.BIG);
	}

	public GameType parseAction(StartHUDAction action){

		switch(action){

			case StartHUDAction.SelectGameType1:	return(GameType.GameType1);

			case StartHUDAction.SelectGameType2:	return(GameType.GameType2);

			case StartHUDAction.SelectGameType3:	return(GameType.GameType3);

			default:								return(GameType.GameType2);
		}
	}

	public void OnAction(StartHUDAction action, Vector3? hitPoint = null){

		switch(action){

			case StartHUDAction.SelectGameType1:
			case StartHUDAction.SelectGameType2:
			case StartHUDAction.SelectGameType3:

				this.SelectGameType(action, hitPoint);
				break;

			case StartHUDAction.SwitchDifficultyLevel:

				this.SwitchDifficultyLevel();
				break;

			case StartHUDAction.SwitchBoardSize:

				this.SwitchBoardSize();
				break;
		}
	}
}
