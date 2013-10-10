
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

		this.newGame.SetText(game.language.newGame);
		this.loading.SetText(game.language.loading);

		this.gameType.SetText(game.language.gameType);
		this.difficulty.SetText(game.language.difficulty);
		this.boardSize.SetText(game.language.boardSize);

		switch(game.difficulty){

			case GameDifficulty.EASY:

				this.difficultyValue.SetText(game.language.easy);
				break;

			case GameDifficulty.MEDIUM:

				this.difficultyValue.SetText(game.language.medium);
				break;

			case GameDifficulty.HARD:

				this.difficultyValue.SetText(game.language.hard);
				break;
		}

		switch(game.boardSize){

			case BoardSize.SMALL:

				this.boardSizeValue.SetText(game.language.small);
				break;

			case BoardSize.REGULAR:

				this.boardSizeValue.SetText(game.language.regular);
				break;

			case BoardSize.BIG:

				this.boardSizeValue.SetText(game.language.big);
				break;
		}
	}

}
