
using UnityEngine;


[System.Serializable]
public class HUDRectangle{

	public float left;
	public float top;
	public float width;
	public float height;

	/* constructor */
	public HUDRectangle(float left, float top, float width, float height){

		this.left	= left;
		this.top	= top;
		this.width	= width;
		this.height	= height;
	}

	/* returns an absolute rectangle by using given position/size as container */
	public Rect RelativeRect(float offsetLeft, float offsetTop, float totalWidth, float totalHeight){

		float x = offsetLeft + this.left * totalWidth;
		float y = offsetTop + this.top * totalHeight;
		float w = this.width * totalWidth;
		float z = this.height * totalHeight;

		return( new Rect(x, y, w, z) );
	}

	/* returns an absolute rectangle by using another rectangle as container */
	public Rect RelativeRect(Rect relativeRect){

		return( this.RelativeRect(relativeRect.x, relativeRect.y, relativeRect.width, relativeRect.height) );
	}

	/* converts this scaled rectangle into an absolute rectangle by using the screen as container */
	public static implicit operator Rect(HUDRectangle that){

		return( that.RelativeRect(0, 0, Screen.width, Screen.height) );
	}
}
