
using UnityEngine;


[System.Serializable]
public class MemoryGameStatsCards{

	public int	total;
	public int	matches;
	public int	failures;

	public void Reset(int total){

		this.total		= total;
		this.matches	= 0;
		this.failures	= 0;
	}
}
