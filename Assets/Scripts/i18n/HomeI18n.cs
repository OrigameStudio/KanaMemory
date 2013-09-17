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

		this.play.text = this.game.language.play;
		this.dojo.text = this.game.language.dojo;
		this.loading.text = this.game.language.loading;
		this.languageName.text = this.game.language.name;
		this.languageIcon.texture = this.game.language.texture;
	}
}
