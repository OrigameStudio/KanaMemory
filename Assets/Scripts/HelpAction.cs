
using UnityEngine;
using System.Collections;


public enum HelpActionType{ LearnHiragana, LearnKatakana, Exit, Stay, Previous, Next, Switch, Zoom, ConfirmExit };

public class HelpAction : HUDText{

	public HelpControl control;
	public HelpActionType type;

	public override void OnClick(int mouseButton){

		switch(this.type){

			case HelpActionType.LearnHiragana:

				this.control.SwitchToHiragana();
				break;

			case HelpActionType.LearnKatakana:

				this.control.SwitchToKatakana();
				break;

			case HelpActionType.Exit:

				this.control.Exit();
				break;

			case HelpActionType.Stay:

				this.control.Stay();
				break;

			case HelpActionType.Previous:

				this.control.Previous();
				break;

			case HelpActionType.Next:

				this.control.Next();
				break;

			case HelpActionType.Switch:

				this.control.Switch();
				break;

			case HelpActionType.Zoom:

				this.control.ToggleZoom();
				break;

			case HelpActionType.ConfirmExit:

				this.control.ConfirmExit();
				break;
		}
	}
}
