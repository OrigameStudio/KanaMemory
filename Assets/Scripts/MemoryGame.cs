
using UnityEngine;
using System.Collections;


public class MemoryGame : MonoBehaviour{

	public GameType type = GameType.GameType1;

	void Start(){

		GameObject.DontDestroyOnLoad(this.gameObject);

	}

	public static MemoryGame GetInstance(){

		return( (MemoryGame)GameObject.FindObjectOfType( typeof(MemoryGame) ) );
	}
}
