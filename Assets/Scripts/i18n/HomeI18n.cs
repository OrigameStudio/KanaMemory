using UnityEngine;
using System.Collections;

public class HomeI18n : MonoBehaviour {

	private MemoryGame game;

	public HUDText play;
	public HUDText dojo;
	public HUDText loading;

	void Start(){

		this.game = MemoryGame.GetInstance();

		this.UpdateLanguage();
	}

	public void UpdateLanguage(){

		this.play.text = this.game.language.play;
		this.dojo.text = this.game.language.dojo;
		this.loading.text = this.game.language.loading;
	}
}
