
using UnityEngine;
using System.Collections;


public class GameControl : MonoBehaviour{

	public GameControlPositions		positions;
	public GameControlSounds		sounds;
	public GameControlScenes		scenes;
	public GameControlBoards		boards;
	public GameControlDecks			decks;

	private MemoryGame	memoryGame;

	private bool	first		= true;
	private bool	failed		= false;
	private Card	card1		= null;
	private Card	card2		= null;
	private bool	isTicking	= false;

	void Start(){

		Board board;

		this.memoryGame = MemoryGame.GetInstance();

		board = this.boards.FindBoard(this.memoryGame.boardSize);

		board.Reset(this.memoryGame.type, this.decks.FindDecks(this.memoryGame.difficulty) );

		this.memoryGame.StartGame(board.pairs, board.seconds);

		Debug.Log("board.pairs: " + board.pairs + ", board.seconds: " + board.seconds);
	}

	public void Succeed(){

		Application.LoadLevel(this.scenes.success);
	}

	public void Failed(){

		Application.LoadLevel(this.scenes.failure);
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

				this.animation.Play("Game@Failure");
			}
		}

		if( Input.GetMouseButtonDown(0) ){

			Ray rayo;
			RaycastHit info;

			rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

			if( Physics.Raycast (rayo, out info) ){

				Card card = info.collider.gameObject.GetComponent<Card>();

				this.SelectCard(card);
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

		}else if(this.card1 != card){

			card.FlipFaceUp();

			this.card2 = card;
			this.first = true;

			this.failed = !this.memoryGame.SelectCards(this.card1, this.card2);

			if(this.failed){

				this.card1.Failure();
				this.card2.Failure();

			}else{

				this.card1.Success(this.positions.card1, this.positions.observer);
				this.card2.Success(this.positions.card2, this.positions.observer);
			}
		}
	}

}
