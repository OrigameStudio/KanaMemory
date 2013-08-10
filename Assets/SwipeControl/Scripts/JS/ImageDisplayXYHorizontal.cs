
///////////////////////////////////////////////
//// 	  ImageDisplayXYHorizontal.js      ////
////  copyright (c) 2011 by Markus Hofer   ////
////          for GameAssets.net           ////
///////////////////////////////////////////////

using UnityEngine;
using System.Collections;


public class ImageDisplayXYHorizontal : MonoBehaviour{

	public SwipeControlXY swipeCtrl;

//IMAGES
	//Array of Images, all images need to have the same dimensions!
	public Texture2D[] img = new Texture2D[0];
	//Leave empty to use img-dimensions and place in the center of the matrix - this can be used to offset the image from the center of the matrix!
	public Rect imgRect;

//MATRIX
	//Check this to move the matrix to the center of the screen (any value in MatrixPosition will be added to this = offset from center)
	public bool  centerMatrixOnScreen = true;
	//This is the center of the matrix, use this to position the control - everything will rotate around this point!
	public Vector3 matrixPosition = Vector3.zero;
	private Vector3 prevMatrixPosition;
	//Use this to rotate the GUI //formerly known as globalAngle
	public float matrixAngle = 0.0f;
	//used to check if the Angle changed, so the Quaternion doens't have to be calculated every frame
	private float previousAngle;
	private Quaternion quat = Quaternion.identity;
	private Matrix4x4 matrix;
	//Use the full width (left and right of the image) for swiping?
	public bool  expandInputAreaToFullWidth = false;

//OPTIONS
	//Display all images, don't fade out the unselected ones...
	public bool  displayAll = false;

//DOTS
	//Dots - First is inactive, second is active. Set Array-length to 0 to hide the dots entirely
	public Texture2D[] dot = new Texture2D[2];
	//Position of the dots, relative to the center of imgRect
	public Vector2 dotRelativeCenterPos;

//DEBUG OPTIONS
	//Show Debug Stuff
	public bool  debug = false;
	public GUIStyle debugBoxStyle;

	void Awake ()
	{

		if (!swipeCtrl){
			
			//Find SwipeControl on same GameObject if none given
			swipeCtrl = gameObject.GetComponent<SwipeControlXY> ();
		}

		if(!dot[0] || !dot[1]){
			
			Debug.LogWarning ("If you want to hide the dots, set the dot array length to 0 and don't just leave the slots empty!");
		}

		if(imgRect == new Rect (0, 0, 0, 0)){
			
			//If no rect given, create default rect
			imgRect = new Rect (-img [0].width * 0.5f, -img [0].height * 0.5f, img [0].width, img [0].height);
		}
	
		//Set up SwipeControl
		swipeCtrl.pxDistBetweenValues.x = img [0].width;
		swipeCtrl.maxValue.x = img.Length - 1;
		
		if(expandInputAreaToFullWidth){
			
			// Use image-height for the input-Rect, but full screen-width
			swipeCtrl.SetActiveArea (new Rect (-Screen.width * 0.5f, imgRect.y, Screen.width, imgRect.height));
			
		}else{
			
			//Use the same Rect as the images for input
			swipeCtrl.SetActiveArea (imgRect);
		}
		swipeCtrl.CalculateEdgeRectsFromActiveArea (imgRect);
		swipeCtrl.Setup ();
	
		//Determine center position of the Dots
		if(dotRelativeCenterPos == Vector2.zero){
			
			dotRelativeCenterPos.y = imgRect.height * 0.5f + 14;
		}
		
		dotRelativeCenterPos = new Vector2( (imgRect.x + imgRect.width * 0.5f) + dotRelativeCenterPos.x, (imgRect.y + imgRect.height * 0.5f) + dotRelativeCenterPos.y );

		if(centerMatrixOnScreen){
			
			matrixPosition.x += Mathf.Round (Screen.width * 0.5f);
			matrixPosition.y += Mathf.Round (Screen.height * 0.5f);
		}
	
	}
	
	void SetGuiColorAlpha(float alpha){
		
		Color tmp = GUI.color;
		tmp = new Color(tmp.r, tmp.g, tmp.b, alpha);
		
		GUI.color = tmp;
	}

	void OnGUI(){
	
		// GUI MATRIX
		if(matrixAngle != previousAngle || matrixPosition != prevMatrixPosition){
			
			//only calculate new Quaternion if angle changed
			quat.eulerAngles = new Vector3 (0.0f, 0.0f, matrixAngle);
			previousAngle = matrixAngle;
			//If you're no longer tweaking
			matrix = Matrix4x4.TRS (matrixPosition, quat, Vector3.one);
			prevMatrixPosition = matrixPosition;
			// Tell SwipeControl to use the same Matrix we use here
			swipeCtrl.matrix = matrix;
		}
		GUI.matrix = matrix;
		
		// IMAGES
		if(displayAll){
			
			//display all images
			for(int j = 0; j < img.Length; j++){
				
				GUI.DrawTexture( new Rect (imgRect.x + imgRect.width * j - swipeCtrl.smoothValue.x * imgRect.width, imgRect.y, imgRect.width, imgRect.height), img [j] );
			}
				                
		}else{
			
			//only display the selected one and fade the previous and next image in and out
			float offset = swipeCtrl.smoothValue.x - Mathf.Round (swipeCtrl.smoothValue.x);
			float mainPos = imgRect.x - (offset * imgRect.width);
		
			if(Mathf.Round (swipeCtrl.smoothValue.x) >= 0 && Mathf.Round (swipeCtrl.smoothValue.x) < img.Length){
				
				//GUI.color.a = 1 - Mathf.Abs (offset);
				this.SetGuiColorAlpha( 1 - Mathf.Abs(offset) );
				GUI.DrawTexture (new Rect (mainPos, imgRect.y, imgRect.width, imgRect.height), img [Mathf.RoundToInt (swipeCtrl.smoothValue.x)]);
			}
			
			//GUI.color.a = -offset;
			this.SetGuiColorAlpha(-offset);
			if (GUI.color.a > 0.0f && Mathf.Round (swipeCtrl.smoothValue.x) - 1 >= 0 && Mathf.Round (swipeCtrl.smoothValue.x) - 1 < img.Length) {
				GUI.DrawTexture (new Rect (mainPos - imgRect.width, imgRect.y, imgRect.width, imgRect.height), img [Mathf.RoundToInt (swipeCtrl.smoothValue.x) - 1]);
			}
			
			//GUI.color.a = offset;
			this.SetGuiColorAlpha(offset);
			if (GUI.color.a > 0.0f && Mathf.Round (swipeCtrl.smoothValue.x) + 1 < img.Length && Mathf.Round (swipeCtrl.smoothValue.x) + 1 >= 0) {
				GUI.DrawTexture (new Rect (mainPos + imgRect.width, imgRect.y, imgRect.width, imgRect.height), img [Mathf.RoundToInt (swipeCtrl.smoothValue.x) + 1]);
			}
			
			//GUI.color.a = 1.0f;
			this.SetGuiColorAlpha(1.0f);
	
		}

		// DOTS
		if(dot.Length > 0){
			
			for(float i = 0; i < img.Length; i++){
				
				bool activeOrNot = false;
				if(i == Mathf.Round (swipeCtrl.smoothValue.x)){
					activeOrNot = true; 
				}
				
				if(!activeOrNot){
					
					GUI.DrawTexture (new Rect (dotRelativeCenterPos.x - (img.Length * dot [0].width * 0.5f) + (i * dot [0].width), Mathf.Round (dotRelativeCenterPos.y - (dot [0].height * 0.5f)), dot [0].width, dot [0].height), dot [0]);
					
				}else{
					
					GUI.DrawTexture (new Rect (Mathf.Round (dotRelativeCenterPos.x - (img.Length * dot [0].width * 0.5f) + (i * dot [0].width)), Mathf.Round (dotRelativeCenterPos.y - (dot [0].height * 0.5f)), dot [0].width, dot [0].height), dot [1]);
				}
				
				//GUI.Toggle(new Rect((centerPos.x - (dotStatusArray.Length * 13 * factor * 0.5f) + (i * 13 * factor)), (centerPos.y - (13 * factor * 0.5f)), 13 * factor, 13 * factor), activeOrNot, GUIContent.none, guiStyle);	
			}	
		}

	
		//If you have the BlackishGUI-script you can simply call the following function instead of the above code
		//BlackishGUI.Dots(dotRelativeCenterPos, img.Length, Mathf.Round(swipeCtrl.smoothValue.x));
		
		//DEBUG info
		if(debug){
			
			GUI.Box(new Rect (-2, -2, 4, 4), GUIContent.none, debugBoxStyle);
			GUI.Box(imgRect, "imgRect with matrix", debugBoxStyle);	
			GUI.Box(swipeCtrl.leftEdgeRectForClickSwitch, "<<", debugBoxStyle);
			GUI.Box(swipeCtrl.rightEdgeRectForClickSwitch, ">>", debugBoxStyle);
			GUI.Box(swipeCtrl.activeArea, "activeArea", debugBoxStyle);

			GUI.matrix = Matrix4x4.identity;
			GUI.Label(new Rect (Input.mousePosition.x + 15, Screen.height - Input.mousePosition.y - 15, 200, 100), "screen-mouse: " + Input.mousePosition.x + ", " + Input.mousePosition.y + "\nscreen-touch + gui: " + Input.mousePosition.x + ", " + (Screen.height - Input.mousePosition.y));

			Vector3 mPos = new Vector3 (Input.mousePosition.x - (Screen.width * 0.5f), Input.mousePosition.y - (Screen.height * 0.5f), 0.0f);
			Vector3 tmPos = swipeCtrl.matrix.MultiplyPoint3x4 (mPos);
			Vector2 tmPosC = new Vector2 (tmPos.x - (Screen.width * 0.5f), tmPos.y - (Screen.height * 0.5f));
			Vector2 ttPosC = new Vector2 (tmPos.x - (Screen.width * 0.5f), (Screen.height - tmPos.y) - (Screen.height * 0.5f));
			GUI.Label (new Rect (Input.mousePosition.x + 15, Screen.height - Input.mousePosition.y + 15, 200, 100), "matrix-mouse: " + Mathf.Round (tmPosC.x) + ", " + Mathf.Round (tmPosC.y) + "\nmatrix-touch + gui: " + Mathf.Round (ttPosC.x) + ", " + (Mathf.Round (ttPosC.y)));
		}
	
	}
}