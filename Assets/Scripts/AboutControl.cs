
using UnityEngine;
using System.Collections;


public class AboutControl : SplashControl{

	public string tumblr	= "http://origamestudio.tumblr.com/";
	public string mail		= "mailto:origamestudio@gmail.com?subject=MemorizeKana";
	public string facebook	= "http://www.facebook.com/OrigameStudio";
	public string twitter	= "http://twitter.com/OrigameStudio";

	public void GoTo(string url){

		Application.OpenURL(url);
	}

	public void OnAction(AboutHUDAction action){

		switch(action){

			case AboutHUDAction.Exit:

				this.Skip(null);
				break;

			case AboutHUDAction.Tumblr:

				this.GoTo(this.tumblr);
				break;

			case AboutHUDAction.Mail:

				this.GoTo(this.mail);
				break;

			case AboutHUDAction.Facebook:

				this.GoTo(this.facebook);
				break;

			case AboutHUDAction.Twitter:

				this.GoTo(this.twitter);
				break;

			case AboutHUDAction.RateApp:

				MemoryGame.GetInstance().RateApp();
				break;
		}
	}
}
