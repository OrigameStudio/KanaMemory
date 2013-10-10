
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

	private GUIStyle currentGUIStyle = null;
	private string currentGUIStyleId = null;
	private string currentRectId = null;

#if UNITY_EDITOR

	public int				debugFontSize	= -1;
	public int				debugFontCalls	= -1;
	public int				debugFontFrame	= -1;
	public string			debugGUIStyleId	= null;
	public string			debugRectId		= null;
	public int				debugChanges	= -1;

#endif

	void Start(){

#if UNITY_EDITOR

			this.debugChanges = 0;

#endif

		// Precalculate GUIStyle
		this.SetText(this.text);
	}

	public void Prepare(){

		this.GetCurrentGUIStyle( this.CalculateRectangle() );
	}

	public void SetText(string text){

		this.text = text;

		this.Prepare();
	}

	/*
	private string GetGUIStyleId(Rect rectangle){

		return(
			this.text			+ "<" +
			( this.font.family == null ? "null" : this.font.family.name)
								+ "|" +
			this.font.style		+ "," +
			this.font.size		+ ">" +
			//this.crop			+ "/" +
			this.wordWrap		+ "@" +
			//rectangle.x		+ "," +
			//rectangle.y		+ ":" +
			rectangle.width		+ "x" +
			rectangle.height 	+ "@" +
			Screen.width		+ "x" +
			Screen.height
		);
	}
	*/

	private string GetGUIStyleId(){

		return(
			this.text			+ "<" +
			( this.font.family == null ? "null" : this.font.family.name)
								+ "|" +
			this.font.style		+ "," +
			this.font.size		+ ">" +
			//this.crop			+ "/" +
			this.wordWrap
		);
	}

	private string GetRectId(Rect rectangle){

		return(
			//rectangle.x		+ "," +
			//rectangle.y		+ ":" +
			rectangle.width		+ "x" +
			rectangle.height 	+ "@" +
			Screen.width		+ "x" +
			Screen.height
		);
	}

	private Rect CalculateRectangle(){

		return( this.padding.Pad(this.rectangle) );
	}

	private GUIStyle CalculateGUIStyle(Rect rectangle, bool sameRect){

		GUIStyle style = new GUIStyle();

		style.clipping = (this.crop ? TextClipping.Clip : TextClipping.Overflow);
		style.alignment = this.alignment;
		style.wordWrap = this.wordWrap;

		if(this.font != null){

			this.font.ApplyTo(style);

			if(this.font.size == 0){

				this.CalculateFontSize(style, rectangle, sameRect);
			}

		}

		return(style);
	}

	public void CalculateFontSize(GUIStyle style, Rect rectangle, bool sameRect){

		GUIContent content = new GUIContent(this.text);

		int size = (sameRect ? this.currentGUIStyle.fontSize + 1 : 1);
		int step = (sameRect ? 4 : 16);

		Debug.Log("LET'S START WITH: " + size + " @ " + step + " (" + sameRect + ")");

#if UNITY_EDITOR

		this.debugFontCalls = 0;

#endif

		if(this.wordWrap){

			float guiHeight = rectangle.height - 1;

			for(style.fontSize = size; rectangle.height > guiHeight; size += step){

#if UNITY_EDITOR

				this.debugFontCalls++;

				Debug.Log("LET'S TRY WITH: " + size);
#endif
				style.fontSize = size;
				guiHeight = style.CalcHeight(content, rectangle.width);
			}

			if(rectangle.height < guiHeight){

				for(size = style.fontSize - 1; rectangle.height < guiHeight; size--){

#if UNITY_EDITOR

					this.debugFontCalls++;

					Debug.Log("LET'S TRY AGAIN BACKWARDS WITH: " + size);

#endif
					style.fontSize = size;
					guiHeight = style.CalcHeight(content, rectangle.width);
				}
			}

		}else{

			Vector2 guiSize = new Vector2(rectangle.width - 1, rectangle.height - 1);

			for(style.fontSize = size; rectangle.width > guiSize.x && rectangle.height > guiSize.y; size += step){

#if UNITY_EDITOR

				Debug.Log("LET'S TRY WITH: " + size);

				this.debugFontCalls++;

#endif
				style.fontSize = size;
				guiSize = style.CalcSize(content);
			}

			if(rectangle.width < guiSize.x || rectangle.height < guiSize.y){

				for(size = style.fontSize - 1; rectangle.width < guiSize.x || rectangle.height < guiSize.y; size--){

#if UNITY_EDITOR

					Debug.Log("LET'S TRY AGAIN BACKWARDS WITH: " + size);

					this.debugFontCalls++;

#endif
					style.fontSize = size;
					guiSize = style.CalcSize(content);
				}
			}
		}

		Debug.Log("WE FOUND: " + style.fontSize);
	}

	private GUIStyle GetCurrentGUIStyle(Rect rectangle){

		string styleId	= this.GetGUIStyleId();
		string rectId	= this.GetRectId(rectangle);

		bool sameRect = ( this.currentRectId != null && this.currentRectId.Equals(rectId) );

		if(this.currentGUIStyle == null || !this.currentGUIStyleId.Equals(styleId) || !sameRect){

			this.currentGUIStyle	= this.CalculateGUIStyle(rectangle, sameRect);
			this.currentGUIStyleId	= styleId;
			this.currentRectId		= rectId;

#if UNITY_EDITOR

			this.debugChanges++;
			this.debugFontFrame		= Time.frameCount;
			this.debugFontSize		= this.currentGUIStyle.fontSize;
			this.debugGUIStyleId	= this.currentGUIStyleId;
			this.debugRectId		= this.currentRectId;

#endif
		}

		return(this.currentGUIStyle);
	}

	void OnGUI(){

		if( !this.Applies() || this.doNotRender ) return;

		Rect position;
		GUIStyle style;

		GUI.depth = this.guiDepth;

		this.DebugOnGUI();

		position	= this.CalculateRectangle();
		style		= this.GetCurrentGUIStyle(position);

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
