
using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class HUDTexture : HUDElement{

	public Texture			texture;
	public HUDScaleModeEnum	scaleMode	= HUDScaleModeEnum.ScaleToFit;
	public bool				alphaBlend	= true;
	public float			imageAspect	= 0;
	public HUDAlignment		alignment	= new HUDAlignment(HUDAlignmentX.Center, HUDAlignmentY.Middle);

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

	void OnGUI(){

		if( !this.Applies() ) return;

		this.DebugOnGUI();

		Rect position = this.rectangle;

		position = this.padding.Pad(position);

		if(this.scaleMode == HUDScaleModeEnum.NoScale){

			position = this.alignment.Align( position, this.GetImageRectangle() );

		}else if(this.scaleMode == HUDScaleModeEnum.ScaleToFit){

			position = this.alignment.Align( position, this.GetImageAspect() );
		}

		if(this.texture != null){

			GUI.DrawTexture(position, this.texture, new HUDScaleMode(this.scaleMode), this.alphaBlend, this.imageAspect);
		}
	}
}
