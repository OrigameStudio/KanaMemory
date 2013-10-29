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

		/* font{ */
			this.welcome.font.family = language.font;
			this.studyHiragana.font.family = language.font;
			this.studyKatakana.font.family = language.font;
			this.goodbye.font.family = language.font;
			this.exit.font.family = language.font;
			this.stay.font.family = language.font;
		/* } */

		this.welcome.SetText(language.welcome);
		this.studyHiragana.SetText(language.studyHiragana);
		this.studyKatakana.SetText(language.studyKatakana);
		this.goodbye.SetText(language.goodbye);
		this.exit.SetText(language.exit);
		this.stay.SetText(language.stay);
	}
}
