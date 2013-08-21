
using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameHUD{

	public HUDGamePlay	gamePlay;
	public HUDPause		pause;
	
	public void PauseGame(){

		this.gamePlay.Disable();
		this.pause.Enable();
	}

	public void ResumeGame(){

		this.gamePlay.Enable();
		this.pause.Disable();
	}
}
