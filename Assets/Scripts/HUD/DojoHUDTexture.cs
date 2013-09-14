
using UnityEngine;
using System.Collections;


public class DojoHUDTexture : HUDTexture{

	public DojoControl control;
	public DojoHUDAction action;

	public override void OnClick(int mouseButton){

		this.control.OnAction(this.action);
	}
}
