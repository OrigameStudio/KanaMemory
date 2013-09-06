
using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class HUDText : HUDElement{

	public string			text;
	/* line spacing, tab size */
	public HUDFont			font;
	public Color			color			= Color.black;
	public Color			shadowColor		= Color.gray;
	public TextAnchor		alignment		= TextAnchor.MiddleCenter;
	public bool				crop			= false;
	public bool				wordWrap		= true;
	public int				shadowOffset	= 0;

	//public GUIStyle stylette;

	/*
	public HUDScaleModeEnum	scaleMode	= HUDScaleModeEnum.ScaleToFit;
	public bool				alphaBlend	= true;
	public float			imageAspect	= 0;
	public HUDAlignment		alignment	= new HUDAlignment(HUDAlignmentX.Center, HUDAlignmentY.Middle);
	*/

	/*
	public float GetImageAspect(){

		if(this.imageAspect != 0f){

			return(this.imageAspect);
		}

		if(this.texture == null){

			return(0f);
		}

		return(this.texture.width / this.texture.height);
	}

	public Rect GetImageRectangle(){

		return( new Rect(0, 0, this.texture.width, this.texture.height) );
	}
	*/

	public void AutoFontSize(GUIStyle style, Rect rectangle){

		GUIContent content = new GUIContent(this.text);

		int size = 1;

		if(this.wordWrap){

			float guiHeight = rectangle.height;

			for(style.fontSize = 1; rectangle.height >= guiHeight; size++, style.fontSize = size){

				guiHeight = style.CalcHeight(content, rectangle.width);
			}

		}else{

			Vector2 guiSize = new Vector2(rectangle.width, rectangle.height);

			for(style.fontSize = 1; rectangle.width >= guiSize.x && rectangle.height >= guiSize.y; size++, style.fontSize = size){

				guiSize = style.CalcSize(content);
			}
		}

		style.fontSize = (size - 1);
	}

	void OnGUI(){

		if( !this.Applies() ) return;

		GUI.depth = this.guiDepth;

		this.DebugOnGUI();

		Rect position = this.rectangle;

		position = this.padding.Pad(position);

		GUIStyle style = new GUIStyle();

		if(this.font != null){

			this.font.ApplyTo(style);
		}

		style.clipping = (this.crop ? TextClipping.Clip : TextClipping.Clip);
		style.alignment = this.alignment;
		style.wordWrap = this.wordWrap;

		if(this.font != null && this.font.size == 0){

			this.AutoFontSize(style, position);
		}

		if(this.shadowOffset != 0){

			Rect shadowPosition = new Rect(position.x + this.shadowOffset, position.y + this.shadowOffset, position.width, position.height);

			style.normal.textColor = this.shadowColor;

			GUI.Label(shadowPosition, this.text, style);
		}

		style.normal.textColor = this.color;

		if( GUI.Button(position, this.text, style) ){

			if(this.isClickable && (!Application.isEditor || Application.isPlaying) ){

				this.OnClick(0);
			}
		}
	}
}
