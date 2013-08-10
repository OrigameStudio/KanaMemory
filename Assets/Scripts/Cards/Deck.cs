
using UnityEngine;
using System.Collections.Generic;


public class Deck : MonoBehaviour{

	public Alphabet alphabet = Alphabet.HIRAGANA;

	public Card[] GetCards(){

		return( this.gameObject.GetComponentsInChildren<Card>() );
	}

	public static List<Card> GetCards(List<Deck> decks){

		List<Card>	cards;

		cards = new List<Card>();

		foreach(Deck deck in decks){

			cards.AddRange( deck.GetCards() );
		}

		return(cards);
	}

}
