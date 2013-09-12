
using UnityEngine;
using System.Collections;


public class HomeControl : SplashScreenControl{

	public void Exit(){

		this.exitApplication = true;
		this.Skip(null);
	}
}
