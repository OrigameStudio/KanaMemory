
using UnityEngine;
using System.Collections;

public class HomeHUDButton : HUDTexture{

	public HomeControl control;

	public override void OnClick(int mouseButton){

		this.control.Exit();
	}
}
