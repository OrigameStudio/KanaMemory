
using UnityEngine;
using System.Collections;


public class StartI18n : MonoBehaviour{

	public HUDText newGame;
	public HUDText loading;

	void Start(){

		this.UpdateLanguage( MemoryGame.GetInstance().language );
	}

	public void UpdateLanguage(GameLanguageData language){

		this.newGame.text = language.newGame;
		this.loading.text = language.loading;
	}
}
