
using UnityEngine;
using System.Collections;


public class GameHUD : MonoBehaviour{
	
	public HUDElement[]	gameInfo;
	public HUDElement[]	pauseInfo;

	public HUDText		cardsInfo;
	public HUDText		timeInfo;
	public HUDTexture	typeInfo;
	public HUDTexture	hintButton;
	public HUDTexture	exitButton;

	public Texture2D	type1;
	public Texture2D	type2;
	public Texture2D	type3;

	public Texture2D	hintEnable;
	public Texture2D	hintDisable;
	
	private MemoryGame memoryGame;
	
	void Start(){
		
		this.memoryGame = MemoryGame.GetInstance();
		
		this.typeInfo.texture	= this.GetGameTypeTexture();
	}

	void Update(){

		int seconds = this.memoryGame.GetSecondsLeft();
		int minutes	= Mathf.FloorToInt(seconds / 60);

		if(minutes > 0){

			seconds -= (minutes * 60);
		}
		
		this.cardsInfo.SetText( this.memoryGame.cards.matches + "/" + this.memoryGame.cards.total );
		this.timeInfo.SetText( string.Format("{0}:{1:d2}", minutes, seconds) );
		
		this.hintButton.texture	= (this.memoryGame.hint ? this.hintDisable : this.hintEnable);

	}

	private Texture2D GetGameTypeTexture(){

		switch(this.memoryGame.type){

			case GameType.GameType1:

				return(this.type1);

			case GameType.GameType2:

				return(this.type2);

			case GameType.GameType3:

				return(this.type3);
		}

		return(null);
	}
		
	public void Pause(){
		
		HUDElement.DisableRender(this.gameInfo);
		HUDElement.EnableRender(this.pauseInfo);
	}

	public void Resume(){
		
		HUDElement.EnableRender(this.gameInfo);
		HUDElement.DisableRender(this.pauseInfo);
	}
}
