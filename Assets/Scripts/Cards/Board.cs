
using UnityEngine;
using System.Collections.Generic;


public class Board : MonoBehaviour{

	public BoardSize	size	= BoardSize.REGULAR;
	public int			pairs	= 15;
	public int			seconds	= 300;

	public void FilterDecks(GameType type, Deck[] decks, out List<Deck> decks1, out List<Deck> decks2){

		List<Deck>	decksHiragana;
		List<Deck>	decksKatakana;
		List<Deck>	decksRomaji;

		decksHiragana	= new List<Deck>();
		decksKatakana	= new List<Deck>();
		decksRomaji		= new List<Deck>();

		foreach(Deck deck in decks){

			switch(deck.alphabet){

				case Alphabet.HIRAGANA:
					decksHiragana.Add(deck);
					break;

				case Alphabet.KATAKANA:
					decksKatakana.Add(deck);
					break;

				case Alphabet.ROMAJI:
					decksRomaji.Add(deck);
					break;
			}
		}

		decks1 = null;
		decks2 = null;

		switch(type){

			case GameType.GameType1:
				decks1 = decksHiragana;
				decks2 = decksRomaji;
				break;

			case GameType.GameType2:
				decks1 = decksKatakana;
				decks2 = decksRomaji;
				break;

			case GameType.GameType3:
				decks1 = decksHiragana;
				decks2 = decksKatakana;
				break;
		}
	}

	public void FilterCards(GameType type, Deck[] decks, out List<Card> cards1, out List<Card> cards2){

		List<Deck>	decks1;
		List<Deck>	decks2;

		this.FilterDecks(type, decks, out decks1, out decks2);

		cards1 = Deck.GetCards(decks1);
		cards2 = Deck.GetCards(decks2);
	}

	public static void ShuffleCards(List<Card> cards){

		int index1 = cards.Count;

		while(index1 > 1){

			Card value;
			int index2;

			index1--;
			index2 = Random.Range(0, index1 + 1);

			value			= cards[index2];
			cards[index2]	= cards[index1];
			cards[index1]	= value;
    	}
	}

	public List<Card> SelectRandomCards(GameType type, Deck[] decks){

		List<Card>	selection;
		List<Card>	cards1;
		List<Card>	cards2;
		int			count;

		this.FilterCards(type, decks, out cards1, out cards2);

		Board.ShuffleCards(cards1);
		Board.ShuffleCards(cards2);

		selection = new List<Card>(this.pairs * 2);

		count = 0;
		foreach(Card card1 in cards1){

			if(card1 != null){

				Card card2 = Card.FindMatchingCard(cards2, card1);

				if(card2 != null){

					selection.Add(card1);
					selection.Add(card2);
					count++;
				}

				if(count == this.pairs) break;
			}
		}

		Board.ShuffleCards(selection);

		return(selection);
	}

	public void Reset(GameType type, Deck[] decks){

		int			totalCards;
		Card[]		boardCards;
		List<Card>	deckCards;

		totalCards	= this.pairs * 2;
		boardCards	= this.gameObject.GetComponentsInChildren<Card>();
		deckCards	= this.SelectRandomCards(type, decks);

		for(int index = 0; index < totalCards; index++){

			boardCards[index].Reset( deckCards[index] );
		}

	}
}
