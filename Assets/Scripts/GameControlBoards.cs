
using UnityEngine;
using System.Collections;


[System.Serializable]
public class GameControlBoards{

	public GameObject small;
	public GameObject regular;
	public GameObject big;

	public Board InstantiateBoard(BoardSize size){

		GameObject	prefab	= null;
		Board		board	= null;

		switch(size){

			case BoardSize.SMALL:

				prefab = this.small;
				break;

			case BoardSize.REGULAR:

				prefab = this.regular;
				break;

			case BoardSize.BIG:

				prefab = this.big;
				break;
		}

		if(prefab != null){

			board = ( (GameObject)GameObject.Instantiate(prefab) ).GetComponent<Board>();
		}

		return(board);
	}
}
