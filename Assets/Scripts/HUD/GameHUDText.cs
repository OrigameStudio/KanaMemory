
using UnityEngine;
using System.Collections;


public class GameHUDText : HUDText{

	public GameControl control;
	public GameHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.onAction(this.action);
	}
}
