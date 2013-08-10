
using UnityEngine;

[System.Serializable]
public class HUDFont{

	public Font			family;
	public FontStyle	style;
	public int			size;

	/* constructor */
	public HUDFont(Font family, FontStyle style, int size){

		this.family	= family;
		this.style	= style;
		this.size	= size;
	}

	/* converts this scaled rectangle into an absolute rectangle by using the screen as container */
	public void ApplyTo(GUIStyle style){

		style.font		= this.family;
		style.fontSize	= this.size;
		style.fontStyle	= this.style;
	}
}
