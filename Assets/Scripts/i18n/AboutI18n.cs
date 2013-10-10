
using UnityEngine;
using System.Collections;


public class AboutI18n : MonoBehaviour{

	public HUDText rateThisApp;

	void Start(){

		this.UpdateLanguage( MemoryGame.GetInstance().language );
	}

	public void UpdateLanguage(GameLanguageData language){

		this.rateThisApp.SetText(language.rateThisApp);
	}
}
