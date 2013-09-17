
using UnityEngine;
using System.Collections;


[System.Serializable]
public class GameLanguageData{

	public string		name			= "English";
	public Texture2D	texture;

	public string		loading			= "Loading...";

	/* home */
	public string		play			= "PLAY";
	public string		dojo			= "DOJO";

	/* dojo */
	public string		welcome			= "WELCOME TO\nTHE DOJO";
	public string		studyHiragana	= "STUDY\nHIRAGANA";
	public string		studyKatakana	= "STUDY\nKATAKANA";
	public string		goodbye			= "EXIT\nDOJO?";
	public string		exit			= "YES";
	public string		stay			= "NO";

	/* start */
	public string		newGame			= "New Game";

	/* game */
	public string		confirmQuitGame	= "QUIT?";
	public string		quitGame		= "YES";
	public string		keepPlaying		= "NO";

	/* ok */
	public GameObject	Mesh_OK;

	/* ko */
	public GameObject	Mesh_KO;
}
