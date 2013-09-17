
using UnityEngine;
using System.Collections;


public class GameAvailableLanguages : MonoBehaviour{

	public GameLanguage defaultLanguage;

	public bool forceDefaultLanguage = false;

	public GameLanguage[] languages;

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

			foreach(GameLanguage language in this.languages){

				if(language.system == systemLanguage){

					return(language);
				}
			}
		}

		return(this.defaultLanguage);
	}
}
