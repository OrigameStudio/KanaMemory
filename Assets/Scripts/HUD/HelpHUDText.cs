
using UnityEngine;
using System.Collections;


public class HelpHUDText : HUDText{

	public HelpControl control;
	public HelpHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.OnAction(this.action);
	}
}
