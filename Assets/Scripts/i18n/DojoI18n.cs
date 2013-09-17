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

		this.welcome.text		= language.welcome;
		this.studyHiragana.text	= language.studyHiragana;
		this.studyKatakana.text	= language.studyKatakana;
		this.goodbye.text		= language.goodbye;
		this.exit.text			= language.exit;
		this.stay.text			= language.stay;
	}
}
