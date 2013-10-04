
using UnityEngine;
using System.Collections;


public class AboutHUDTexture : HUDTexture{

	public AboutControl control;
	public AboutHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.OnAction(this.action);
	}
}
