
using UnityEngine;
using System.Collections;

public class GameHUDTexture : HUDTexture{

	public GameControl control;

	public GameHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.onAction(this.action);
	}
}
