
using UnityEngine;
using System.Collections;


public class GameHUDExit : HUDTexture{

	public GameControl control;

	public override void OnClick(int mouseButton){

		this.control.PauseGame();
	}
}
