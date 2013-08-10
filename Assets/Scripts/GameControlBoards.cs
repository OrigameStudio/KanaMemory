
using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameControlBoards{

	public Board tiny;
	public Board small;
	public Board medium;
	public Board big;
	public Board huge;

	public Board FindBoard(BoardSize size){

		switch(size){

			case BoardSize.TINY:	return(this.tiny);
			case BoardSize.SMALL:	return(this.small);
			case BoardSize.MEDIUM:	return(this.medium);
			case BoardSize.BIG:		return(this.big);
			case BoardSize.HUGE:	return(this.tiny);
		}

		return(null);
	}
}
