
using UnityEngine;
using System.Collections;


public class GameAvailableLanguages : MonoBehaviour{

	public static string PREFERRED_LANGUAGE = "PreferredLanguage";

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

	void Start(){

		this.GetGameLanguage();
	}

	public void SavePreferredLanguage(){

		PlayerPrefs.SetInt(PREFERRED_LANGUAGE, this.currentLanguage);
	}

	private GameLanguage GetPreferredLanguage(){

		int preferredLanguage;

		preferredLanguage = PlayerPrefs.GetInt(PREFERRED_LANGUAGE, -1);

		if(preferredLanguage >= 0 && preferredLanguage < this.languages.Length){

			GameLanguage lang = this.languages[preferredLanguage];

			this.currentLanguage = preferredLanguage;

			Debug.Log("User preferred language: " + preferredLanguage + " (" + lang.system + ")");

			return(lang);
		}

		return(null);
	}

	private GameLanguage FindGameLanguage(SystemLanguage systemLanguage){

		for(this.currentLanguage = 0; this.currentLanguage < this.languages.Length; this.currentLanguage++){

			GameLanguage language = this.languages[this.currentLanguage];

			if(language.system == systemLanguage){

				return(language);
			}
		}

		return(null);
	}

	public GameLanguage GetGameLanguage(){

		GameLanguage language;

		if(!this.forceDefaultLanguage){

			language = this.GetPreferredLanguage();

			if(language != null){

				return(language);
			}

			language = this.FindGameLanguage(Application.systemLanguage);

			if(language != null){

				return(language);
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
