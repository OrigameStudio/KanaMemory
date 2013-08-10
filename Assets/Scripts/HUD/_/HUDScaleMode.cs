
using UnityEngine;


public enum HUDScaleModeEnum{
	NoScale,
	ScaleToFit,
	StretchToFill,
	ScaleAndCrop
}


public sealed class HUDScaleMode{

	private readonly HUDScaleModeEnum scaleMode;

	public HUDScaleMode(HUDScaleModeEnum scaleMode){

		this.scaleMode = scaleMode;
	}

	/* converts this hud scale mode into a native scale mode */
	public static implicit operator ScaleMode(HUDScaleMode that){

		switch(that.scaleMode){

			case HUDScaleModeEnum.NoScale:
			case HUDScaleModeEnum.ScaleToFit:
				return(ScaleMode.ScaleToFit);

			case HUDScaleModeEnum.StretchToFill:
				return(ScaleMode.StretchToFill);

			case HUDScaleModeEnum.ScaleAndCrop:
				return(ScaleMode.ScaleAndCrop);
		}

		return(ScaleMode.ScaleToFit);
	}
}

