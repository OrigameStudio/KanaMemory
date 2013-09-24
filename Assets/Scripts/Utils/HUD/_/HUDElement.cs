
using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class HUDElement : MonoBehaviour{

	public int				guiDepth	= 0;
	public bool				debug		= false;
	public Color			debugColor	= Color.magenta;
	public HUDOrientation	orientation	= HUDOrientation.Any;
	public HUDRectangle		rectangle	= new HUDRectangle(0, 0, 1, 1);
	public HUDPadding		padding		= new HUDPadding(0, 0, 0, 0);
	public bool				isClickable	= false;

	private float			clicked		= 0f;

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

			if(this.padding.left != 0 || this.padding.top != 0 || this.padding.right != 0 || this.padding.bottom != 0){

				this.DrawBox(this.padding.Pad(this.rectangle), Color.white);
			}
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

	public virtual void OnClick(int mouseButton){

		Debug.Log("[" + Time.time + "] Game object '" + this.gameObject.name + "' was clicked at " + Input.mousePosition.x + "," + Input.mousePosition.y + " (mouse button: " + mouseButton + ")");
	}

	public void ProcessClicks(Rect rectangle){

		int mouseButton = 0;
		bool click = false;

		if( Input.GetMouseButtonDown(0) ){

			click = true;

		}else /* if(this.anyMouseButton) */{

			if( Input.GetMouseButtonDown(1) ){

				mouseButton = 1;
				click = true;

			}else if( Input.GetMouseButtonDown(2) ){

				mouseButton = 2;
				click = true;
			}
		}

		if(click){

			Vector2 mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

			if(		mousePosition.x >= rectangle.x
				&&	mousePosition.x <= rectangle.x + rectangle.width
				&&	mousePosition.y >= rectangle.y
				&&	mousePosition.y <= rectangle.y + rectangle.height
			){

				float now = Time.time;

				if(this.clicked != now){

					this.clicked = now;

					this.OnClick(mouseButton);
				}
			}
		}
	}
}
