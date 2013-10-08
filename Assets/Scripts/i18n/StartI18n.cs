
using UnityEngine;
using System.Collections;


public class StartI18n : MonoBehaviour{

	public HUDText newGame;
	public HUDText loading;

	public HUDText gameType;

	public HUDText difficulty;
	public HUDText difficultyValue;

	public HUDText boardSize;
	public HUDText boardSizeValue;

	public void UpdateLanguage(MemoryGame game){

		this.newGame.text		= game.language.newGame;
		this.loading.text		= game.language.loading;

		this.gameType.text		= game.language.gameType;
		this.difficulty.text	= game.language.difficulty;
		this.boardSize.text		= game.language.boardSize;

		switch(game.difficulty){

			case GameDifficulty.EASY:

				this.difficultyValue.text = game.language.easy;
				break;

			case GameDifficulty.MEDIUM:

				this.difficultyValue.text = game.language.medium;
				break;

			case GameDifficulty.HARD:

				this.difficultyValue.text = game.language.hard;
				break;
		}

		switch(game.boardSize){

			case BoardSize.SMALL:

				this.boardSizeValue.text = game.language.small;
				break;

			case BoardSize.REGULAR:

				this.boardSizeValue.text = game.language.regular;
				break;

			case BoardSize.BIG:

				this.boardSizeValue.text = game.language.big;
				break;
		}
	}

}
