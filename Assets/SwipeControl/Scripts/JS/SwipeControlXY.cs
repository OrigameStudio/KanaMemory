///////////////////////////////////////////////
////   	      SwipeControlXY.js            ////
////  copyright (c) 2011 by Markus Hofer   ////
////          for GameAssets.net           ////
///////////////////////////////////////////////
////									   ////
//// This is a version of SwipeControl     ////
//// that works in 2 axis! (horizontally   ////
//// and vertically) 					   ////
////  									   ////
///////////////////////////////////////////////

using UnityEngine;
using System.Collections;


public class SwipeControlXY : MonoBehaviour{
	
	//turn this control on or off
	public bool  controlEnabled = true;
	
	//If you set it up from another script you can skip the auto-setup! - Don't forget to call Setup() though!!
	public bool  skipAutoSetup = false;
	
	//If you don't want to allow mouse or touch input and only want to use this control for animating a value, set this to false.
	public bool  allowInput = true;
	
	//When mouse-controlled, should a simple click on either side of the control increases/decreases the value by one?
	public bool  clickEdgeToSwitch = true;

	// how many pixels do you have to move the mouse/finger in each direction to get to the next value? - this was called partWidth before...
	public Vector2 pxDistBetweenValues = new Vector2 (200f, 50f);
	
	// calculated once in the beginning, so we don't have to do costly divisions every frame
	private Vector2 partFactor = new Vector2 (1f, 1f);
	
	// start with this value
	public Vector2 startValue = Vector2.zero;
	
	// current value
	public Vector2 currentValue = Vector2.zero;
	
	//max value
	public Vector2 maxValue = new Vector2 (10, 10);
	
	//where you can click to start the swipe movement (once you clicked you can drag outside as well) - used to be called MouseRect
	public Rect activeArea;
	public Rect leftEdgeRectForClickSwitch = new Rect ();
	public Rect rightEdgeRectForClickSwitch = new Rect ();
	public Matrix4x4 matrix = Matrix4x4.identity;
	
	//dragging operation in progress?
	private bool  touched = false;
	
	//set to 1 for each finger that starts touching the screen within our touchRect
	protected int[] fingerStartArea = new int[5];
	
	//set to 1 if mouse starts clicking within touchRect
	private int mouseStartArea = 0;
	
	//current smooth value between 0 and maxValue
	public Vector2 smoothValue = Vector2.zero;
	private Vector2 smoothStartPos = Vector2.zero;
	
	//how far (% of the width of one element) do we have to drag to set it to change currentValue?
	private Vector2 smoothDragOffset = new Vector2 (0.2f, 0.2f);
	private float lastSmoothValue;
	private float[] prevSmoothValueX = new float[5];
	private float[] prevSmoothValueY = new float[5];
	
	//needed to make Mathf.SmoothDamp work even if Time.timeScale == 0
	private float realtimeStamp;
	
	//current velocity of Mathf.SmoothDamp()
	private float xVelocity;
	private float yVelocity;
	
	//Clamp the maximum speed of Mathf.SmoothDamp()
	public Vector2 maxSpeed = new Vector2 (20.0f, 20.0f);
	public float bufferZone = 0.12f;
	private Vector2 mStartPos;
	
	//Touch/Mouse Position turned into a Vector3
	private Vector3 pos;
	
	//transformed Position
	private Vector2 tPos;
	
	public bool debug = false;

	IEnumerator Start(){
	
		if(this.clickEdgeToSwitch && !this.allowInput){
			
			Debug.LogWarning("You have enabled clickEdgeToSwitch, but it will not work because allowInput is disabled!", this);
		}

		if(this.pxDistBetweenValues.x == 0f && this.pxDistBetweenValues.y == 0f){
			
			Debug.LogWarning("pxDistBetweenValues is zero - you won't be able to swipe with this setting...", this);	
		}
			
		yield return new WaitForSeconds(0.2f);
	
		if(!this.skipAutoSetup){
			
			this.Setup ();
		}
	}

	public void Setup(){
		
		this.partFactor.x = 1.0f / this.pxDistBetweenValues.x;
		this.partFactor.y = 1.0f / this.pxDistBetweenValues.y;
		
		//Set smoothValue to the currentValue - tip: setting currentValue to -1 and startValue to 0 makes the first image appear at the start...
		this.smoothValue.x = Mathf.Round(this.currentValue.x);
		this.smoothValue.y = Mathf.Round(this.currentValue.y);
		
		//Apply Start-value
		this.currentValue = this.startValue;
	
		if(this.activeArea != new Rect(0, 0, 0, 0) ){
			
			this.SetActiveArea(this.activeArea);	
		}
					
		if( this.leftEdgeRectForClickSwitch == new Rect(0, 0, 0, 0) ){
			
			//Only do this if not set in the inspector	
			this.CalculateEdgeRectsFromActiveArea();
		}

		if(this.matrix == Matrix4x4.zero){
			
			this.matrix = Matrix4x4.identity.inverse;
		}
	}

	public void SetActiveArea(Rect myRect){
		
		this.activeArea = myRect;
	}

	public void CalculateEdgeRectsFromActiveArea(){
		
		this.CalculateEdgeRectsFromActiveArea(this.activeArea);
	}

	public void CalculateEdgeRectsFromActiveArea(Rect myRect){
		
		this.leftEdgeRectForClickSwitch.x = myRect.x;
		this.leftEdgeRectForClickSwitch.y = myRect.y;
		this.leftEdgeRectForClickSwitch.width = myRect.width * 0.5f;
		this.leftEdgeRectForClickSwitch.height = myRect.height;
	
		this.rightEdgeRectForClickSwitch.x = myRect.x + myRect.width * 0.5f;
		this.rightEdgeRectForClickSwitch.y = myRect.y;
		this.rightEdgeRectForClickSwitch.width = myRect.width * 0.5f;
		this.rightEdgeRectForClickSwitch.height = myRect.height;	
	}

	private void SetEdgeRects(Rect leftRect, Rect rightRect){
		
		this.leftEdgeRectForClickSwitch = leftRect;
		this.rightEdgeRectForClickSwitch = rightRect;		
	}

	private float GetAvgValue(float[] arr){
	
		float sum = 0.0f;
		for(int i = 0; i < arr.Length; i++){
			
			sum += arr[i];
		}
		
		return(sum / arr.Length);
	}

	private void FillArrayWithValue(float[] arr, float val){
	
		for(int i = 0; i < arr.Length; i++){
			
			arr [i] = val;	
		}
	
	}

	void Update(){
	
		if(this.controlEnabled){
		
			this.touched = false;
		
			if(this.allowInput){
			
				if( Input.GetMouseButton(0) || Input.GetMouseButtonUp(0) ){
					
					this.pos = new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 0.0f);
					this.tPos = this.matrix.inverse.MultiplyPoint3x4(pos);
				
					//BEGAN
					if( Input.GetMouseButtonDown(0) && activeArea.Contains(tPos) ){
						
						this.mouseStartArea = 1;	
					}
				
					//WHILE MOUSEDOWN
					if(this.mouseStartArea == 1){
						
						this.touched = true;
						
						//START
						if( Input.GetMouseButtonDown(0) ){
							
							this.mStartPos = tPos;
							this.smoothStartPos.x = this.smoothValue.x + this.tPos.x * this.partFactor.x;	
							this.smoothStartPos.y = this.smoothValue.y + this.tPos.y * this.partFactor.y;	
							this.FillArrayWithValue(this.prevSmoothValueX, this.smoothValue.x);
							this.FillArrayWithValue(this.prevSmoothValueY, this.smoothValue.y);
						}
						
						//DRAGGING
						this.smoothValue.x = this.smoothStartPos.x - this.tPos.x * this.partFactor.x;
						this.smoothValue.y = this.smoothStartPos.y - this.tPos.y * this.partFactor.y;
						
						if(this.smoothValue.x < -this.bufferZone){
							
							this.smoothValue.x = -this.bufferZone;
							
						}else if(this.smoothValue.x > this.maxValue.x + this.bufferZone){
							
							this.smoothValue.x = this.maxValue.x + this.bufferZone;
						}
						
						if(this.smoothValue.y < -this.bufferZone){
							
							this.smoothValue.y = -this.bufferZone;
							
						}else if(this.smoothValue.y > this.maxValue.y + this.bufferZone){
							
							this.smoothValue.y = this.maxValue.y + this.bufferZone;
						}
						
						//END
						if( Input.GetMouseButtonUp(0) ){
							
							if( (this.tPos - this.mStartPos).sqrMagnitude < 25 ){
								
								if(this.clickEdgeToSwitch){
									
									if( this.leftEdgeRectForClickSwitch.Contains(this.tPos) ){
										
										this.currentValue.x -= 1f;
										
										if(this.currentValue.x < 0f){
											
											this.currentValue.x = 0f;
										}
										
									}else if( this.rightEdgeRectForClickSwitch.Contains(this.tPos) ){
										
										this.currentValue.x += 1f;
										
										if(this.currentValue.x > this.maxValue.x){
											
											currentValue.x = maxValue.x;
										}
									}
								}
							}else{
								
								if( this.currentValue.x - (this.smoothValue.x + (this.smoothValue.x - this.GetAvgValue(this.prevSmoothValueX))) > this.smoothDragOffset.x || this.currentValue.x - (this.smoothValue.x + (this.smoothValue.x - this.GetAvgValue(this.prevSmoothValueX))) < -this.smoothDragOffset.x ){
									
									//dragged beyond dragOffset to the right
									this.currentValue.x = Mathf.Round(  this.smoothValue.x + (this.smoothValue.x - this.GetAvgValue(this.prevSmoothValueX) )  );
									this.xVelocity = ( this.smoothValue.x - this.GetAvgValue(prevSmoothValueX) ); // * -0.10f ;
									
									if(this.currentValue.x > this.maxValue.x){
										
										this.currentValue.x = this.maxValue.x;
										
									}else if(this.currentValue.x < 0){
										
										this.currentValue.x = 0;
									}
								}
								
								if( this.currentValue.y - (this.smoothValue.y + (this.smoothValue.y - this.GetAvgValue(this.prevSmoothValueY))) > this.smoothDragOffset.y || this.currentValue.y - (this.smoothValue.y + (this.smoothValue.y - this.GetAvgValue(this.prevSmoothValueY))) < -this.smoothDragOffset.y ){
									
									//dragged beyond dragOffset to the right
									this.currentValue.y = Mathf.Round(  this.smoothValue.y + (this.smoothValue.y - GetAvgValue(this.prevSmoothValueY) )  );
									this.yVelocity = ( this.smoothValue.y - this.GetAvgValue(this.prevSmoothValueY) ); // * -0.10f ;
									
									if(this.currentValue.y > this.maxValue.y){
										
										this.currentValue.y = this.maxValue.y;
										
									}else if(this.currentValue.y < 0){
										
										currentValue.y = 0;
									}
								}									
							}
							
							this.mouseStartArea = 0;
						}
						
						for(int i = 1; i < this.prevSmoothValueX.Length; i++){
							
							this.prevSmoothValueX[i] = this.prevSmoothValueX[i - 1];
							this.prevSmoothValueY[i] = this.prevSmoothValueY[i - 1];
						}
						
						this.prevSmoothValueX[0] = this.smoothValue.x;
						this.prevSmoothValueY[0] = this.smoothValue.y;
					}
				}

			#if UNITY_IPHONE || UNITY_ANDROID

			foreach(Touch touch in Input.touches){
				pos = Vector3(touch.position.x, Screen.height - touch.position.y, 0.0f);
				tPos = matrix.inverse.MultiplyPoint3x4(pos);		
		
				//BEGAN
				print(tPos + " inside " + activeArea + "?");
				if (touch.phase == TouchPhase.Began && activeArea.Contains(tPos)) {
					fingerStartArea[touch.fingerId] = 1;
					print("hit!");
				}
				//WHILE FINGER DOWN
				if(fingerStartArea[touch.fingerId] == 1) { // no touchRect.Contains check because once you touched down you're allowed to drag outside...
					touched = true;
					//START
					if(touch.phase == TouchPhase.Began) {
						smoothStartPos.x = smoothValue.x + tPos.x * partFactor.x;
						FillArrayWithValue(prevSmoothValueX, smoothValue.x);
						smoothStartPos.y = smoothValue.y + tPos.y * partFactor.y;
						FillArrayWithValue(prevSmoothValueY, smoothValue.y);
					}
					//DRAGGING
					smoothValue.x = smoothStartPos.x - tPos.x * partFactor.x;
					smoothValue.y = smoothStartPos.y - tPos.y * partFactor.y;
					if(smoothValue.x < -bufferZone) { smoothValue.x = -bufferZone; }
					else if(smoothValue.x > maxValue.x + bufferZone) { smoothValue.x = maxValue.x + bufferZone; }
					if(smoothValue.y < -bufferZone) { smoothValue.y = -bufferZone; }
					else if(smoothValue.y > maxValue.y + bufferZone) { smoothValue.y = maxValue.y + bufferZone; }
					//END
					if(touch.phase == TouchPhase.Ended) {
							if(currentValue.x - (smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX))) > smoothDragOffset.x || currentValue.x - (smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX))) < -smoothDragOffset.x){ //dragged beyond dragOffset to the right
								currentValue.x = Mathf.Round(smoothValue.x + (smoothValue.x - GetAvgValue(prevSmoothValueX)));
								xVelocity = (smoothValue.x - GetAvgValue(prevSmoothValueX)); // * -0.10f ;
								if(currentValue.x > maxValue.x) currentValue.x = maxValue.x;
								else if(currentValue.x < 0f) currentValue.x = 0f;					
							}							
							if(currentValue.y - (smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY))) > smoothDragOffset.y || currentValue.y - (smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY))) < -smoothDragOffset.y){ //dragged beyond dragOffset to the right
								currentValue.y = Mathf.Round(smoothValue.y + (smoothValue.y - GetAvgValue(prevSmoothValueY)));
								yVelocity = (smoothValue.y - GetAvgValue(prevSmoothValueY)); // * -0.10f ;
								if(currentValue.y > maxValue.y) currentValue.y = maxValue.y;
								else if(currentValue.y < 0f) currentValue.y = 0f;					
							}							

						}		
						for(i = 1; i < prevSmoothValueX.Length; i++) {
							prevSmoothValueX[i] = prevSmoothValueX[i- 1];
							prevSmoothValueY[i] = prevSmoothValueY[i- 1];
						}
						prevSmoothValueX[0] = smoothValue.x;
						prevSmoothValueY[0] = smoothValue.y;
				}
		
		
				if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) fingerStartArea[touch.fingerId] = 0;
		
			}
			#endif

			}
	
			if(!this.touched){
				
				this.smoothValue.x = Mathf.SmoothDamp(this.smoothValue.x, this.currentValue.x, ref this.xVelocity, 0.3f, this.maxSpeed.x, Time.realtimeSinceStartup - this.realtimeStamp);
				this.smoothValue.y = Mathf.SmoothDamp(this.smoothValue.y, this.currentValue.y, ref this.yVelocity, 0.3f, this.maxSpeed.y, Time.realtimeSinceStartup - this.realtimeStamp);
			} 
			
			this.realtimeStamp = Time.realtimeSinceStartup;	
		}
	
	}

	void OnGUI(){
		
		if(this.debug){
			
			if(Input.touchCount > 0){
				
				GUI.Label(
					new Rect(Input.GetTouch(0).position.x + 15, Screen.height - Input.GetTouch(0).position.y - 60, 200, 100),
					"pos : " + this.pos + "\ntPos: " + this.tPos
				);
			}
			
			/*
			GUI.Label(
				
				new Rect(Input.mousePosition.x + 15, Screen.height - Input.mousePosition.y - 60, 200, 100),
				"mPos : " + mPos + "\ntmPos: " + tmPos + "\ntPos: " + tPos
			);
			*/
			
			GUI.matrix = this.matrix;
			
			GUI.Box(this.activeArea, GUIContent.none);
		}
	}

}
