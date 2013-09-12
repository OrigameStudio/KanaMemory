
using UnityEngine;


[System.Serializable]
public class MemoryGameStatsTime{

	public float	start;
	public int		total;
	public int		left;

	public void Reset(int total){

		this.start = Time.time;
		this.total = total;
	}
}
