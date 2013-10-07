
using UnityEngine;
using System.Collections;


[System.Serializable]
public class GameControlBoards{

	public Board small;
	public Board regular;
	public Board big;

	public Board InstantiateBoard(BoardSize size){

		Board prefab	= null;
		Board board		= null;

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

			board = (Board)GameObject.Instantiate(prefab);
		}

		return(board);
	}
}
