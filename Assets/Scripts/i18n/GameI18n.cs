
using UnityEngine;
using System.Collections;


public class GameI18n : MonoBehaviour{

	public HUDText		confirmQuitGame;
	public HUDText		quitGame;
	public HUDText		keepPlaying;

	void Start(){

		this.UpdateLanguage( MemoryGame.GetInstance().language );
	}

	public void UpdateLanguage(GameLanguageData language){

		this.confirmQuitGame.text	= language.confirmQuitGame;
		this.quitGame.text			= language.quitGame;
		this.keepPlaying.text		= language.keepPlaying;
	}
}
