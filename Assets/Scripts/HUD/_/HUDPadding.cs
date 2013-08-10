
using UnityEngine;


[System.Serializable]
public class HUDPadding{

	public float top;
	public float left;
	public float bottom;
	public float right;

	/* constructor */
	public HUDPadding(float top, float left, float bottom, float right){

		this.top	= top;
		this.left	= left;
		this.bottom	= bottom;
		this.right	= right;
	}

	/* returns a modified rectangle */
	public Rect Pad(Rect rectangle){

		return(
			new Rect(
				rectangle.x + this.left,
				rectangle.y + this.top,
				rectangle.width - (this.left + this.right),
				rectangle.height - (this.top + this.bottom)
			)
		);
	}

}
