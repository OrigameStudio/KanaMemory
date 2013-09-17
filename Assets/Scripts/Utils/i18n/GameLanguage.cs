
using UnityEngine;
using System.Collections;


[System.Serializable]
public class GameLanguage : MonoBehaviour{

	public SystemLanguage		system	= SystemLanguage.English;
	public string				caption	= "English";
	public Texture2D			texture;

	public GameLanguageData		data;
}
