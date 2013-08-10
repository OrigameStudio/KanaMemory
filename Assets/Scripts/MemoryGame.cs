﻿
using UnityEngine;
using System.Collections;


public class MemoryGame : MonoBehaviour{

	public GameStatus		status		= GameStatus.Ready;
	public GameType			type		= GameType.GameType1;
	public GameDifficulty	difficulty	= GameDifficulty.MEDIUM;
	public BoardSize		boardSize	= BoardSize.MEDIUM;
	public GameStats		cards;
	public int				games;
	public TimeStats		time;

	public static MemoryGame GetInstance(){

		return( (MemoryGame)GameObject.FindObjectOfType( typeof(MemoryGame) ) );
	}

	void Start(){

		GameObject.DontDestroyOnLoad(this.gameObject);
	}

	public void StartGame(int pairs, int seconds){

		this.games++;
		this.time.Reset(seconds);
		this.cards.Reset(pairs);

		this.status = GameStatus.Playing;
	}

	public void UpdateTimeLeft(float now){

		this.time.left = this.time.total - Mathf.FloorToInt(now - this.time.start);

		if(this.time.left <= 0 && this.PairsLeft() > 0){

			this.status = GameStatus.Failure;
		}
	}

	public int PairsLeft(){

		return(this.cards.total - this.cards.matches);
	}

	public int GetSecondsLeft(){

		return(this.time.left > 0 ? this.time.left : 0);
	}

	public bool SelectCards(Card card1, Card card2){

		bool success;

		success = card1.Matches(card2);

		if(success){

			this.cards.matches++;

			if(this.PairsLeft() == 0){

				this.status = GameStatus.Success;
			}

		}else{

			this.cards.failures++;
		}

		return(success);
	}
}
