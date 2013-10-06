
using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameControlBoards{

	public Board small;
	public Board regular;
	public Board big;

	public Board FindBoard(BoardSize size){

		switch(size){

			case BoardSize.SMALL:	return(this.small);
			case BoardSize.REGULAR:	return(this.regular);
			case BoardSize.BIG:		return(this.big);
		}

		return(null);
	}
}
