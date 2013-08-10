
using UnityEngine;


public enum HUDAlignmentX{ Center, Left, Right }
public enum HUDAlignmentY{ Middle, Top, Bottom }


[System.Serializable]
public class HUDAlignment{

	public HUDAlignmentX horizontal;
	public HUDAlignmentY vertical;

	/* constructor */
	public HUDAlignment(HUDAlignmentX horizontal, HUDAlignmentY vertical){

		this.horizontal	= horizontal;
		this.vertical	= vertical;
	}

	/* returns a modified rectangle */
	public Rect Align(Rect rectangle, float aspect){

		float	rectangleAspect = (rectangle.height == 0f ? 0f : rectangle.width / rectangle.height);
		float	width;
		float	height;

		if(rectangleAspect > 1){

			/* landscape rectangle */
			height	= rectangle.height;
			width	= height * aspect;

		}else{

			/* portrait rectangle */
			width	= rectangle.width;
			height	= width * aspect;
		}

		return(  this.Align(rectangle, new Rect(0, 0, width, height) )  );
	}

	/* returns a modified rectangle */
	public Rect Align(Rect background, Rect foreground){

		float	left;
		float	top;
		float	width;
		float	height;

		left	= background.x;
		top		= background.y;
		width	= foreground.width;
		height	= foreground.height;

		if(width < background.width){

			if(this.horizontal == HUDAlignmentX.Center){

				left += (background.width - width) / 2;

			}else if(this.horizontal == HUDAlignmentX.Right){

				left += background.width - width;
			}
		}


		if(height < background.height){

			if(this.vertical == HUDAlignmentY.Middle){

				top += (background.height - height) / 2;

			}else if(this.vertical == HUDAlignmentY.Bottom){

				top += background.height - height;
			}
		}

		return( new Rect(left, top, width, height) );
	}
}
