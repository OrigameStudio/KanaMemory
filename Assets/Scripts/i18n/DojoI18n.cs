using UnityEngine;
using System.Collections;

public class DojoI18n : MonoBehaviour{

	public HUDText welcome;
	public HUDText studyHiragana;
	public HUDText studyKatakana;
	public HUDText goodbye;
	public HUDText exit;
	public HUDText stay;

	void Start(){

		this.UpdateLanguage( MemoryGame.GetInstance().language );
	}

	public void UpdateLanguage(GameLanguageData language){

		this.welcome.SetText(language.welcome);
		this.studyHiragana.SetText(language.studyHiragana);
		this.studyKatakana.SetText(language.studyKatakana);
		this.goodbye.SetText(language.goodbye);
		this.exit.SetText(language.exit);
		this.stay.SetText(language.stay);
	}
}
