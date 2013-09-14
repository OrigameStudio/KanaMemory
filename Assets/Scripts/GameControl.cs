
using UnityEngine;
using System.Collections;


public class GameControl : MonoBehaviour{

	public GameControlPositions		positions;
	public GameMusic				music;
	public GameControlSounds		sounds;
	public GameControlScenes		scenes;
	public GameControlBoards		boards;
	public GameControlDecks			decks;
	public GameHUD					hud;
	public Transform				away;

	private MemoryGame	memoryGame;

	private bool	first		= true;
	private bool	failed		= false;
	private Card	card1		= null;
	private Card	card2		= null;
	private bool	isTicking	= false;
	private bool	isPaused	= false;
	private float	timePaused;

	void Start(){

		Board	board;
		Deck[]	decks;

		this.memoryGame = MemoryGame.GetInstance();

		if(this.memoryGame == null){

			Debug.LogWarning("Cannot find MemoryGame instance!");

			this.QuitGame();

			return;
		}

		board = this.boards.FindBoard(this.memoryGame.boardSize);

		if(board == null){

			Debug.LogWarning("Cannot find board: " + this.memoryGame.boardSize);

			this.QuitGame();

			return;
		}

		decks = this.decks.InstantiateDecks(this.memoryGame.difficulty, this.away);

		board.Reset(this.memoryGame.type, decks);

		this.decks.DeleteDecks(decks);

		this.memoryGame.StartGame(board.pairs, board.seconds);

		Debug.Log("board.pairs: " + board.pairs + ", board.seconds: " + board.seconds);

		this.hud.Resume();
	}

	public void Succeed(){

		Application.LoadLevel(this.scenes.success);
	}

	public void Failed(){

		Application.LoadLevel(this.scenes.failure);
	}

	public void QuitGame(){

		Application.LoadLevel(this.scenes.home);
	}

	public void PauseGame(){

		if(!this.isPaused){

			Debug.Log("Pause");

			this.music.enabled = false;

			this.music.audio.Pause();

			this.hud.Pause();

			this.isPaused = true;

			this.timePaused = Time.time;
		}
	}

	public void ResumeGame(){

		if(this.isPaused){

			Debug.Log("Resume");

			this.music.enabled = true;

			this.music.audio.Play();

			this.memoryGame.ExtendTimeLeft(Time.time - this.timePaused);

			this.hud.Resume();

			this.isPaused = false;
		}
	}

	public void ToggleHint(){

		this.memoryGame.hint = !this.memoryGame.hint;
	}

	void Update(){

		if(!this.animation.isPlaying){

			this.memoryGame.UpdateTimeLeft(Time.time);

			if(!this.isTicking){

				if( this.memoryGame.GetSecondsLeft() <= 10 ){

					this.sounds.clock.Play();

					this.isTicking = true;
				}
			}

			if(this.memoryGame.status == GameStatus.Success){

				this.animation.Play("Game@Success");

			}else if(this.memoryGame.status == GameStatus.Failure){

				this.sounds.timeOut.Play();

				this.animation.Play("Game@Failure");
			}
		}

		if( !this.isPaused && Input.GetMouseButtonDown(0) ){

			Ray ray;
			RaycastHit info;

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if( Physics.Raycast(ray, out info) ){

				Card card = info.collider.gameObject.GetComponent<Card>();

				if(card != null){

					this.SelectCard(card);
				}
			}
		}

		if( Input.GetKeyDown(KeyCode.Escape) ){

			if(this.isPaused){

				this.ResumeGame();

			}else{

				this.PauseGame();
			}
		}
	}

	private void SelectCard(Card card){

		if(this.first){

			if(failed){

				if(this.card1 != card){

					this.card1.FlipFaceDown();
				}

				if(this.card2 != card){

					this.card2.FlipFaceDown();
				}

			}else if(this.card1 != null || this.card2 != null){

				GameObject.Destroy(this.card1.gameObject);
				GameObject.Destroy(this.card2.gameObject);
				return;
			}

			card.FlipFaceUp();

			this.card1 = card;
			this.card2 = null;
			this.first = false;

			if(this.memoryGame.hint){

				this.sounds.success.PlayOneShot(this.card1.sound);
			}

		}else if(this.card1 != card){

			card.FlipFaceUp();

			this.card2 = card;
			this.first = true;

			this.failed = !this.memoryGame.SelectCards(this.card1, this.card2);

			if(this.failed){

				if(this.memoryGame.hint){

					this.sounds.success.PlayOneShot(this.card2.sound);

				}else{

					this.sounds.failure.Play();
				}

				this.card1.Failure();
				this.card2.Failure();

			}else{

				this.sounds.success.PlayOneShot(this.card1.sound);

				this.card1.Success(this.positions.card1, this.positions.observer);
				this.card2.Success(this.positions.card2, this.positions.observer);
			}
		}
	}
	
	public void onAction(GameHUDAction action){
		
		switch(action){
			
			case GameHUDAction.Hint:

				this.ToggleHint();
				break;

			case GameHUDAction.Pause:

				this.PauseGame();
				break;

			case GameHUDAction.Resume:

				this.ResumeGame();
				break;

			case GameHUDAction.Quit:

				this.QuitGame();
				break;
		}
	}
}
