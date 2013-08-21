
using UnityEngine;
using System.Collections;


public class HUDPause : MonoBehaviour{

	public HUDText		title;
	public HUDText		buttonYes;
	public HUDText		buttonNo;
	public HUDTexture	background;

	public void Enable(){

		this.title.enabled		= true;
		this.buttonYes.enabled	= true;
		this.buttonNo.enabled	= true;
		this.background.enabled	= true;
	}

	public void Disable(){

		this.title.enabled		= false;
		this.buttonYes.enabled	= false;
		this.buttonNo.enabled	= false;
		this.background.enabled	= false;
	}
}
