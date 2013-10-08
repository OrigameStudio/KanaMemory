
using UnityEngine;
using System.Collections;

public class StartHUDTexture : HUDTexture{

	public StartControl control;

	public StartHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.OnAction(this.action);
	}
}
