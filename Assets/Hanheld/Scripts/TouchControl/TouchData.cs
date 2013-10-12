
using UnityEngine;


[System.Serializable]
public class TouchData{

	/* The position delta since last change */
	public readonly Vector2 deltaPosition;

	/* Amount of time passed since last change */
	public readonly float deltaTime;

	/* The unique index for touch */
	public readonly int fingerId;

	/* Describes the phase of the touch */
	public readonly TouchPhase phase;
	/*
		Began		A finger touched the screen.
		Moved		A finger moved on the screen.
		Stationary	A finger is touching the screen but hasn't moved.
		Ended		A finger was lifted from the screen. This is the final phase of a touch.
		Canceled	The system cancelled tracking for the touch.
	*/

	/* The position of the touch */
	public readonly Vector2 position;

	/* Number of taps */
	public readonly int tapCount;


	public TouchData(Touch that){

		this.deltaPosition	= that.deltaPosition;
		this.deltaTime		= that.deltaTime;
		this.fingerId		= that.fingerId;
		this.phase			= that.phase;
		this.position		= that.position;
		this.tapCount		= that.tapCount;
	}

	public TouchData(Vector2 deltaPosition, float deltaTime, int fingerId, TouchPhase phase, Vector2 position, int tapCount){

		this.deltaPosition	= deltaPosition;
		this.deltaTime		= deltaTime;
		this.fingerId		= fingerId;
		this.phase			= phase;
		this.position		= position;
		this.tapCount		= tapCount;
	}
}
