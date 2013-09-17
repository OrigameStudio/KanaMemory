
using UnityEngine;
using System.Collections;


public class GameAvailableLanguages : MonoBehaviour{

	public GameLanguage defaultLanguage;

	public bool forceDefaultLanguage = false;

	public GameLanguage[] languages;

	private int currentLanguage;

	private static GameAvailableLanguages instance;
	public static GameAvailableLanguages FindInstance(){

		if(GameAvailableLanguages.instance == null){

			GameAvailableLanguages.instance = (GameAvailableLanguages)GameObject.FindObjectOfType( typeof(GameAvailableLanguages) );
		}

		return(GameAvailableLanguages.instance);
	}

	public GameLanguage GetGameLanguage(){

		if(!this.forceDefaultLanguage){

			SystemLanguage systemLanguage = Application.systemLanguage;

			for(this.currentLanguage = 0; this.currentLanguage < this.languages.Length; this.currentLanguage++){

				GameLanguage language = this.languages[this.currentLanguage];

				if(language.system == systemLanguage){

					return(language);
				}
			}
		}

		return(this.defaultLanguage);
	}

	public GameLanguage GetNextGameLanguage(){

		this.currentLanguage++;

		if(this.currentLanguage >= this.languages.Length){

			this.currentLanguage = 0;
		}

		return( this.languages[this.currentLanguage] );
	}
}
