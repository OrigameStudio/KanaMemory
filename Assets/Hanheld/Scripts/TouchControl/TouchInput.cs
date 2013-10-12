
using UnityEngine;
using System.Collections.Generic;


public class TouchInput{

# if !UNITY_EDITOR && ( UNITY_IPHONE || UNITY_ANDROID )

	public static bool IsMultiTouchEnabled(){

		return(Input.multiTouchEnabled);
	}

	public static int GetTouchCount(){

		return(Input.touchCount);
	}

	public static TouchData GetTouch(int index){

		return(  new TouchData( Input.GetTouch(index) )  );
	}

	public static TouchData[] GetTouches(){

		Touch[]			touch	= Input.touches;
		TouchData[]		touches	= new TouchData[touch.Length];

		for(int index = 0; index < touch.Length; index++){

			touches[index] = new TouchData( touch[index] );
		}

		return(touches);
	}

# else

	public static int useMouseButton = 0;

	private	static Vector2	currentPosition	= Vector2.zero;
	private	static Vector2	lastPosition	= Vector2.zero;
	private static float	lastChanged		= 0f;

	private static TouchPhase GetTouchPhase(){

		if( Input.GetMouseButtonDown(TouchInput.useMouseButton) ){

			return(TouchPhase.Began);
		}

		if( Input.GetMouseButtonUp(TouchInput.useMouseButton) ){

			return(TouchPhase.Ended);
		}

		if( !Input.GetMouseButton(TouchInput.useMouseButton) ){

			return(TouchPhase.Canceled);
		}

		if(TouchInput.currentPosition != TouchInput.lastPosition){

			return(TouchPhase.Moved);
		}

		return(TouchPhase.Stationary);
	}

	private static float GetDeltaTime(float now){

		float delta = now - TouchInput.lastChanged;

		if(TouchInput.currentPosition != TouchInput.lastPosition){

			TouchInput.lastChanged = now;
		}

		return(delta);
	}

	public static bool IsMultiTouchEnabled(){

		return(false);
	}

	public static int GetTouchCount(){

		return( Input.GetMouseButton(TouchInput.useMouseButton) || Input.GetMouseButtonUp(TouchInput.useMouseButton) ? 1 : 0 );
	}

	public static TouchData GetTouch(int index){

		TouchInput.currentPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		Vector2		deltaPosition	= TouchInput.currentPosition - TouchInput.lastPosition;
		float		deltaTime		= TouchInput.GetDeltaTime(Time.time);
		int			fingerId		= 0;
		TouchPhase	phase			= TouchInput.GetTouchPhase();
		Vector2		position		= TouchInput.currentPosition;
		int			tapCount		= 1;

		TouchInput.lastPosition = currentPosition;

		return( new TouchData(deltaPosition, deltaTime, fingerId, phase, position, tapCount) );
	}

	public static TouchData[] GetTouches(){

		TouchData[] touches;

		if( TouchInput.GetTouchCount() == 1 ){

			touches = new TouchData[1];

			touches[0] = TouchInput.GetTouch(0);

		}else{

			touches = new TouchData[0];
		}

		return(touches);
	}

# endif

	public static float NormalizeX(float x){

		return(x / Screen.width);
	}

	public static float NormalizeY(float y){

		return(y / Screen.height);
	}

	public static Vector2 Normalize(Vector2 vector){

		return( new Vector2(vector.x / Screen.width, vector.y / Screen.height) );
	}

	public static bool IsLeftSwipe(float x){

		return(x < 0);
	}

	public static bool IsRightSwipe(float x){

		return(x > 0);
	}

	public static bool IsDownSwipe(float y){

		return(y < 0);
	}

	public static bool IsUpSwipe(float y){

		return(y > 0);
	}
}
