
using UnityEngine;
using System.Collections;


public class HelpControl : MonoBehaviour{

	public int escapeScene = 1;

	void Update(){

		if( Input.GetKey(KeyCode.Escape) ){

			Application.LoadLevel(this.escapeScene);
		}
	}
}
