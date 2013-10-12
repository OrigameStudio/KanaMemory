

public class SwipeListenerAdapter : SwipeListener{

	private SwipeListenerHorizontal horizontal;
	private SwipeListenerVertical vertical;

	private float thresoldHorizontal;
	private float thresoldVertical;

	public SwipeListenerAdapter(SwipeListenerHorizontal horizontal, float thresoldHorizontal = 0f){

		this.horizontal			= horizontal;
		this.thresoldHorizontal	= thresoldHorizontal;
	}

	public SwipeListenerAdapter(SwipeListenerVertical vertical, float thresoldVertical = 0f){

		this.vertical			= vertical;
		this.thresoldVertical	= thresoldVertical;
	}

	public SwipeListenerAdapter(SwipeListenerHorizontal horizontal, SwipeListenerVertical vertical, float thresoldHorizontal = 0f, float thresoldVertical = 0f){

		this.horizontal			= horizontal;
		this.vertical			= vertical;

		this.thresoldHorizontal	= thresoldHorizontal;
		this.thresoldVertical	= thresoldVertical;
	}

	public void Swipe(float x, float y, TouchData touch){

		if(this.horizontal != null){

			if(x < -this.thresoldHorizontal){

				this.horizontal.SwipeLeft(x, touch);

			}else if(x > this.thresoldHorizontal){

				this.horizontal.SwipeRight(x, touch);
			}
		}

		if(this.vertical != null){

			if(y < -this.thresoldVertical){

				this.vertical.SwipeUp(y, touch);

			}else if(y > this.thresoldVertical){

				this.vertical.SwipeDown(y, touch);
			}
		}

	}
}
