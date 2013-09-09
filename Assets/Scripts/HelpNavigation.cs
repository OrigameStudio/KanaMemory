
using UnityEngine;
using System.Collections;


public enum HelpNavigationType{ Previous, Next, Switch, Zoom, ConfirmExit };

public class HelpNavigation : HUDTexture{

	public HelpControl control;
	public HelpNavigationType type;

	public override void OnClick(int mouseButton){

		switch(this.type){

			case HelpNavigationType.Previous:

				this.control.Previous();
				break;

			case HelpNavigationType.Next:

				this.control.Next();
				break;

			case HelpNavigationType.Switch:

				this.control.Switch();
				break;

			case HelpNavigationType.Zoom:

				this.control.ToggleZoom();
				break;

			case HelpNavigationType.ConfirmExit:

				this.control.ConfirmExit();
				break;
		}
	}
}
