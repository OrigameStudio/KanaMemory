
using UnityEngine;
using System.Collections;


public class HomeControl : SplashControl{

	public int	HelpScene	= 2;
	public int	StartScene	= 3;

	public HUDText loadingMessage;

	public AudioSource sound;

	public void Exit(){

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

		this.nextScene = this.StartScene;
		this.Skip(null);

		this.sound.Play();
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
		}

	}
}
