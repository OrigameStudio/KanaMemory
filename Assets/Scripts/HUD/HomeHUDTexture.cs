
using UnityEngine;
using System.Collections;

public class HomeHUDTexture : HUDTexture{

	public HomeControl control;

	public HomeHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.OnAction(this.action);
	}
}
