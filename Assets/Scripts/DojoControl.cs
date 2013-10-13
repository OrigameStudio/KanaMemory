
using UnityEngine;
using System.Collections;


public class DojoControl : MonoBehaviour, SwipeListenerHorizontal{

	public SwipeControl swipe;

	public HUDElement[] Welcome;
	public HUDElement[] Navigation;
	public HUDElement[] Goodbye;
	private bool isLearning;
	private bool isLeaving;

	public Transform[] zoomedInPositions;
	public Transform[] zoomedOutPositions;

	public Transform enterPosition;
	public Transform exitPosition;
	public Transform outsidePosition;

	public float moveSpeed		= 0.1f;
	public float rotateSpeed	= 0.01f;
	public float zoomSpeed		= 1f;

	public float zoomIn			= 15f;
	public float zoomOut		= 60f;

	public HUDTexture zoomIcon;
	private bool isZoomed		= false;
	public Texture zoomOutTexture;
	public Texture zoomInTexture;

	public HUDTexture switchIcon;
	private int firstHiraganaIndex		= 0;
	private int firstKatakanaIndex		= 3;
	public Texture switchToHiraganaTexture;
	public Texture switchToKatakanaTexture;

	public int positionIndex = 0;

	public GameObject observer;
	public Camera mainCamera;

	public int escapeScene = 1;

	public AudioSource steps;
	public AudioSource voice;

	private	MoveTo		moveTo;
	private	RotateAs	rotateAs;
	private Zoomer		zoomer;

	private	Transform	currentTarget;

	void Start(){

		this.moveTo		= MoveTo.Add(this.observer.gameObject, this.enterPosition.transform.position, this.moveSpeed);
		this.rotateAs	= RotateAs.Add(this.observer.gameObject, this.enterPosition.transform.rotation, this.rotateSpeed, true);
		this.zoomer		= Zoomer.Add(this.observer.gameObject, this.mainCamera, this.zoomIn, this.zoomOut, this.zoomSpeed, this.rotateAs);

		HUDElement.EnableRender(this.Welcome);
		HUDElement.DisableRender(this.Navigation);
		HUDElement.DisableRender(this.Goodbye);

		this.steps.Play();

		if(this.swipe != null){

			this.swipe.AddListener( new SwipeListenerAdapter(this, 0.3f) );
		}
	}

	void Update(){

		if( Input.GetKeyDown(KeyCode.Escape) ){

			if(this.isLeaving){

				this.Stay();

			}else{

				this.ConfirmExit();
			}

		}else if( this.isLearning && Input.GetMouseButtonDown(0) ){

			Ray ray;
			RaycastHit info;

			ray = this.mainCamera.ScreenPointToRay(Input.mousePosition);

			if( Physics.Raycast(ray, out info) ){

				Card card = info.collider.gameObject.GetComponent<Card>();

				if(card != null){

					// Read aloud
					this.voice.PlayOneShot(card.sound);

					if(this.isZoomed){

						// Look at it; zoom in; zoom out
						this.zoomer.ZoomIn(card.transform);
					}
				}
			}
		}
	}

	private void UpdateCurrentTarget(){

		if(this.isZoomed){

			this.currentTarget		= this.zoomedInPositions[this.positionIndex];

		}else{

			this.currentTarget		= this.zoomedOutPositions[this.positionIndex];
		}

		this.moveTo.target		= this.currentTarget.position;
		this.rotateAs.target	= this.currentTarget.rotation;
	}

	public void SwipeLeft(float x, TouchData touch){

		if(this.isLearning){

			this.Next();
		}
	}

	public void SwipeRight(float x, TouchData touch){

		if(this.isLearning){

			this.Previous();
		}
	}

	public void Next(){

		this.ZoomOut();

		if(this.positionIndex == 5){

			this.positionIndex = 0;

		}else{

			this.positionIndex++;
		}

		this.UpdateSwitchIcon();

		this.observer.animation.Play();
		this.steps.Play();

		this.UpdateCurrentTarget();
	}

	public void Previous(){

		this.ZoomOut();

		if(this.positionIndex == 0){

			this.positionIndex = 5;

		}else{

			this.positionIndex--;
		}

		this.UpdateSwitchIcon();

		this.observer.animation.Play();
		this.steps.Play();

		this.UpdateCurrentTarget();
	}

	public void UpdateSwitchIcon(){

		if(this.positionIndex < 3){

			this.switchIcon.texture = this.switchToKatakanaTexture;

		}else{

			this.switchIcon.texture = this.switchToHiraganaTexture;
		}
	}

	public void Switch(){

		this.ZoomOut();

		if(this.positionIndex < 3){

			this.positionIndex += 3;

		}else{

			this.positionIndex -= 3;
		}

		this.UpdateSwitchIcon();

		this.observer.animation.Play();
		this.steps.Play();

		this.UpdateCurrentTarget();
	}

	public void StudyHiragana(){

		HUDElement.DisableRender(this.Welcome);
		HUDElement.EnableRender(this.Navigation);

		this.isLearning = true;

		this.switchIcon.texture = this.switchToKatakanaTexture;

		this.ZoomOut();

		this.positionIndex = this.firstHiraganaIndex;

		this.observer.animation.Play();
		this.steps.Play();

		this.UpdateCurrentTarget();
	}

	public void StudyKatakana(){

		HUDElement.DisableRender(this.Welcome);
		HUDElement.EnableRender(this.Navigation);

		this.isLearning = true;

		this.switchIcon.texture = this.switchToHiraganaTexture;

		this.ZoomOut();

		this.positionIndex = this.firstKatakanaIndex;

		this.observer.animation.Play();
		this.steps.Play();

		this.UpdateCurrentTarget();
	}

	public void ConfirmExit(){

		HUDElement.DisableRender(this.Welcome);
		HUDElement.DisableRender(this.Navigation);
		HUDElement.EnableRender(this.Goodbye);

		this.isLearning = false;
		this.isLeaving = true;

		this.moveTo.target		= this.exitPosition.transform.position;
		this.rotateAs.target	= this.exitPosition.transform.rotation;

		this.observer.animation.Play();
		this.steps.Play();
	}

	public void Stay(){

		HUDElement.DisableRender(this.Goodbye);
		HUDElement.DisableRender(this.Navigation);
		HUDElement.EnableRender(this.Welcome);

		this.isLearning = false;
		this.isLeaving = false;

		this.moveTo.target		= this.enterPosition.transform.position;
		this.rotateAs.target	= this.enterPosition.transform.rotation;

		this.observer.animation.Play();
		this.steps.Play();
	}

	public void ToggleZoom(){

		if(this.isZoomed){

			this.zoomIcon.texture = this.zoomInTexture;

		}else{

			this.zoomIcon.texture = this.zoomOutTexture;
		}

		this.isZoomed = !this.isZoomed;

		UpdateCurrentTarget();
	}

	public void ZoomOut(){

		if(this.zoomer.isZoomingIn){

			this.zoomer.ZoomOut();
		}

		if(this.isZoomed){

			this.isZoomed = false;

			this.zoomIcon.texture = this.zoomInTexture;
		}
	}

	public void Exit(){

		HUDElement.DisableRender(this.Goodbye);

		this.moveTo.target		= this.outsidePosition.transform.position;
		this.rotateAs.target	= this.outsidePosition.transform.rotation;

		this.steps.Play();
	}

	public void Exited(){

		Application.LoadLevel(this.escapeScene);
	}

	public void OnAction(DojoHUDAction action){

		switch(action){

			case DojoHUDAction.StudyHiragana:

				this.StudyHiragana();
				break;

			case DojoHUDAction.StudyKatakana:

				this.StudyKatakana();
				break;

			case DojoHUDAction.Exit:

				this.Exit();
				break;

			case DojoHUDAction.ConfirmExit:

				this.ConfirmExit();
				break;

			case DojoHUDAction.Stay:

				this.Stay();
				break;

			case DojoHUDAction.Previous:

				this.Previous();
				break;

			case DojoHUDAction.Next:

				this.Next();
				break;

			case DojoHUDAction.Switch:

				this.Switch();
				break;

			case DojoHUDAction.Zoom:

				this.ToggleZoom();
				break;
		}
	}
}
