
using UnityEngine;
using System.Collections;


public class HomeControl : SplashScreenControl{
	
	public int	HelpScene	= 2;
	public int	StartScene	= 3;

	public AudioSource gong;

	public void Exit(){

		this.exitApplication = true;
		this.Skip(null);
	}
	
	public void Dojo(){

		this.nextScene = this.HelpScene;
		this.Skip(null);
	}
	
	public void Play(){

		this.nextScene = this.StartScene;
		this.Skip(null);
	}
	
	public void OnAction(HomeHUDAction action){

		this.gong.Play();

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
