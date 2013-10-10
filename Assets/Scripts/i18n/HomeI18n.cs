using UnityEngine;
using System.Collections;

public class HomeI18n : MonoBehaviour {

	private MemoryGame game;

	public HUDText play;
	public HUDText dojo;
	public HUDText loading;
	public HUDText languageName;
	public HUDTexture languageIcon;

	void Start(){

		this.game = MemoryGame.GetInstance();

		this.UpdateLanguage();
	}

	public void UpdateLanguage(GameLanguage language = null){

		if(language != null){

			this.game.language = language.data;
		}

		this.play.SetText(this.game.language.play);
		this.dojo.SetText(this.game.language.dojo);
		this.loading.SetText(this.game.language.loading);
		this.languageName.SetText(this.game.language.name);
		this.languageIcon.texture = this.game.language.texture;
	}
}
