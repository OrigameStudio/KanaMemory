
using UnityEngine;
using System.Collections;


public enum PauseAction{ None, Pause, Resume, QuitGame };

public class GameHUDPause : HUDText{

	public GameControl control;
	public PauseAction action = PauseAction.None;

	public override void OnClick(int mouseButton){

		switch(this.action){

			case PauseAction.Pause:

				this.control.PauseGame();
				break;

			case PauseAction.Resume:

				this.control.ResumeGame();
				break;

			case PauseAction.QuitGame:

				this.control.QuitGame();
				break;
		}
	}
}
