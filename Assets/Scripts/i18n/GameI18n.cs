
using UnityEngine;
using System.Collections;


public class GameI18n : MonoBehaviour{

	public HUDText		difficulty;
	public HUDText		confirmQuitGame;
	public HUDText		quitGame;
	public HUDText		keepPlaying;

	void Start(){

		this.UpdateLanguage( MemoryGame.GetInstance() );
	}

	public string GetDifficultyValue(MemoryGame game){

		switch(game.difficulty){

			case GameDifficulty.EASY:		return(game.language.easy);
			case GameDifficulty.MEDIUM:		return(game.language.medium);
			case GameDifficulty.HARD:		return(game.language.hard);
		}

		return(null);
	}

	public void UpdateLanguage(MemoryGame game){

		this.difficulty.SetText( this.GetDifficultyValue(game) );
		this.confirmQuitGame.SetText(game.language.confirmQuitGame);
		this.quitGame.SetText(game.language.quitGame);
		this.keepPlaying.SetText(game.language.keepPlaying);
	}
}
