
using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class HUDElement : MonoBehaviour{

	public bool				debug		= false;
	public Color			debugColor	= Color.magenta;
	public HUDOrientation	orientation	= HUDOrientation.Any;
	public HUDRectangle		rectangle	= new HUDRectangle(0, 0, 1, 1);
	public HUDPadding		padding		= new HUDPadding(0, 0, 0, 0);

	public void DrawBox(Rect position, Color color){

		Texture2D texture;
		Texture2D tmp;

		tmp = GUI.skin.box.normal.background;

		texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();

		GUI.skin.box.normal.background = texture;

			GUI.Box(position, GUIContent.none);

		GUI.skin.box.normal.background = tmp;
	}

	public virtual void DebugOnGUI(){

		if(this.debug){

			this.DrawBox(this.rectangle, this.debugColor);

			this.DrawBox(this.padding.Pad(this.rectangle), Color.white);
		}
	}

	public bool IsLandscape(){

		return(Screen.width > Screen.height);
	}

	public bool IsPortrait(){

		return(Screen.width < Screen.height);
	}

	public bool Applies(){

		switch(this.orientation){

			case HUDOrientation.Any:		return(true);
			case HUDOrientation.Landscape:	return( this.IsLandscape() );
			case HUDOrientation.Portrait:	return( this.IsPortrait() );
		}

		return(false);
	}
}
