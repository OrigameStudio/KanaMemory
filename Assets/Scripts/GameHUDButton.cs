
using UnityEngine;
using System.Collections;

public enum GameHUDAction{ Hint, Exit };

public class GameHUDButton : HUDTexture{

	public GameControl control;

	public GameHUDAction action;

	public override void OnClick(int mouseButton){

		switch(this.action){

			case GameHUDAction.Hint:

				this.control.ToggleHint();
				break;

			case GameHUDAction.Exit:

				this.control.PauseGame();
				break;
		}

	}
}
