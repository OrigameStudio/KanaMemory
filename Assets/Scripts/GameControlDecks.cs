
using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameControlDecks{

	public Deck[] easy;
	public Deck[] medium;
	public Deck[] hard;

	public Deck[] InstantiateDecks(GameDifficulty difficulty, Transform transform){

		Deck[] prefabs	= null;
		Deck[] decks	= null;

		switch(difficulty){

			case GameDifficulty.EASY:
			
				prefabs = this.easy;
				break;

			case GameDifficulty.MEDIUM:

				prefabs = this.medium;
				break;

			case GameDifficulty.HARD:

				prefabs = this.hard;
				break;
		}

		if(prefabs != null){

			decks = new Deck[prefabs.Length];

			for(int index = 0; index < decks.Length; index++){

				decks[index] = (Deck)GameObject.Instantiate(prefabs[index], transform.position, transform.rotation);
			}
		}

		return(decks);
	}
	
	public void DeleteDecks(Deck[] decks){

		foreach(Deck deck in decks){

			GameObject.Destroy(deck.gameObject);
		}
	}
}
