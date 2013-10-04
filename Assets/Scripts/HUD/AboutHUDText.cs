
using UnityEngine;
using System.Collections;


public class AboutHUDText : HUDText{

	public AboutControl control;
	public AboutHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.OnAction(this.action);
	}
}
