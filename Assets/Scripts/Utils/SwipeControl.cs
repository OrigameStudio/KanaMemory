
using UnityEngine;
using System.Collections.Generic;


public class SwipeControl : MonoBehaviour{
		
	public bool isSwiping = false;
	
	public List<SwipeListener> listeners = new List<SwipeListener>();

	public Vector2 total = Vector2.zero;
	public Vector2 delta = Vector2.zero;
	
	public Vector2 startPosition	= Vector2.zero;
	public Vector2 currentPosition	= Vector2.zero;
	
	private Vector2 lastPosition	= Vector2.zero;
	
	// Update is called once per frame
	void Update(){
		
		if( TouchInput.GetTouchCount() == 1 ){

			TouchData touch = TouchInput.GetTouch(0);
			
			this.lastPosition		= this.currentPosition;
			this.currentPosition	= touch.position;
			
			switch(touch.phase){
			
				case TouchPhase.Began:
				
					this.startPosition = this.currentPosition;
				
					this.total		= Vector2.zero;
					this.delta		= Vector2.zero;
					this.isSwiping	= true;
				
					break;

				case TouchPhase.Moved:
				
					this.total		= TouchInput.Normalize(this.currentPosition - this.startPosition);
					this.delta		= TouchInput.Normalize(this.currentPosition - this.lastPosition);
				
					break;

				case TouchPhase.Stationary:
				
					this.delta		= Vector2.zero;

					break;

				default:
				//case TouchPhase.Ended:
				//case TouchPhase.Canceled:
				
					foreach(SwipeListener listener in this.listeners){
					
						if(listener != null){

							listener.Swipe(this.total.x, this.total.y, touch);
						}
					}
				
					this.total		= Vector2.zero;
					this.delta		= Vector2.zero;
					this.isSwiping	= false;
				
					break;
			}
		}
	}
	
	public void AddListener(SwipeListener listener){
		
		if( !this.listeners.Contains(listener) ){
			
			this.listeners.Add(listener);
		}
	}
	
	public void RemoveListener(SwipeListener listener){
		
		if( this.listeners.Contains(listener) ){
			
			this.listeners.Remove(listener);
		}
	}
	
}
