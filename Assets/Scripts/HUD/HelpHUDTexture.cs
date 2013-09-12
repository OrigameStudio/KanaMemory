
using UnityEngine;
using System.Collections;


public class HelpHUDTexture : HUDTexture{

	public HelpControl control;
	public HelpHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.OnAction(this.action);
	}
}
