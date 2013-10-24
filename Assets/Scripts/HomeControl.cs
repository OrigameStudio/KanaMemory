
using UnityEngine;
using System.Collections;


public class HomeControl : SplashControl{

	public int	HelpScene	= 2;
	public int	StartScene	= 3;
	public int	AboutScene	= 7;

	public HUDText loadingMessage;

	public AudioSource sound;

	public HomeI18n homeI18n;

	public GameAvailableLanguages availableLanguages;

	private bool switchedLanguage;

	public void Exit(){

		this.DiscardActivityIndicator();

		this.exitApplication = true;
		this.Skip(null);
	}

	public void Dojo(){

		this.loadingMessage.enabled = true;

		this.nextScene = this.HelpScene;
		this.Skip(null);

		this.sound.Play();
	}

	public void Play(){

		this.DiscardActivityIndicator();

		this.nextScene = this.StartScene;
		this.Skip(null);

		this.sound.Play();
	}

	public void About(){

		this.DiscardActivityIndicator();

		this.nextScene = this.AboutScene;
		this.Skip(null);

		this.sound.Play();
	}

	public void SwitchLanguage(){

		GameLanguage language;

		language = this.availableLanguages.GetNextGameLanguage();

		this.homeI18n.UpdateLanguage(language);

		this.switchedLanguage = true;
	}

	public void OnAction(HomeHUDAction action){

		switch(action){

			case HomeHUDAction.Exit:

				this.Exit();
				break;

			case HomeHUDAction.Dojo:

				this.Dojo();
				break;

			case HomeHUDAction.Play:

				this.Play();
				break;

			case HomeHUDAction.About:

				this.About();
				break;

			case HomeHUDAction.SwitchLanguage:

				this.SwitchLanguage();
				return;
		}

		if(this.switchedLanguage){

			this.availableLanguages.SavePreferredLanguage();
		}
	}
}
