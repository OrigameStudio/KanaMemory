
using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameControlDecks{

	public Deck[] easy;
	public Deck[] medium;
	public Deck[] hard;

	public Deck[] FindDecks(GameDifficulty difficulty){

		switch(difficulty){

			case GameDifficulty.EASY:	return(this.easy);
			case GameDifficulty.MEDIUM:	return(this.medium);
			case GameDifficulty.HARD:	return(this.hard);
		}

		return(null);
	}
}
