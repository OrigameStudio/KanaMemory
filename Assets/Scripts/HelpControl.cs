
using UnityEngine;
using System.Collections;


public class HelpControl : MonoBehaviour, SwipeListener{

	public SwipeControl swipe;

	public bool loopPositions = true;

	public Transform[] positions;
	public Transform outside;

	public float moveSpeed = 0.1f;
	public float rotateSpeed = 0.01f;
	public float focusSpeed = 0.5f;

	public float fieldOfView = 60f;
	public float zoom = 50f;

	public int positionIndex = -1;

	public GameObject observer;
	public Camera mainCamera;

	public int escapeScene = 1;

	public AudioSource sounds;

	public GUIText info;

	private Focuser		focuser;
	private	MoveTo		moveTo;
	private	RotateAs	rotateAs;
	private	Transform	currentTarget;
	private	Transform	previousTarget;
	private	Transform	nextTarget;
	private	Transform	altTarget;

	void Start(){

		this.swipe.AddListener(this);

		this.focuser	= Focuser.Add(this.mainCamera.gameObject, this.fieldOfView, this.focusSpeed);
		this.moveTo		= MoveTo.Add(this.observer.gameObject, this.observer.transform.position, this.moveSpeed);
		this.rotateAs	= RotateAs.Add(this.observer.gameObject, this.observer.transform.rotation, this.rotateSpeed, true);

		this.UpdatePositions();
	}

	void Update(){

		if( Input.GetKey(KeyCode.Escape) ){

			Application.LoadLevel(this.escapeScene);
		}

		if( this.swipe.isSwiping ){

			float		x				= this.swipe.total.x;
			float		y				= this.swipe.total.y;
			float		absX			= Mathf.Abs(x);
			float		absY			= Mathf.Abs(y);
			bool		sideways		= absX >= absY;
			Vector3		positionTarget	= this.currentTarget.position;
			Vector3		rotationTarget	= this.currentTarget.rotation.eulerAngles;
			Vector3		positionOffset	= Vector3.zero;

			if(sideways && absX >= 0.1){

				if( TouchInput.IsLeftSwipe(x) ){

					positionTarget = this.nextTarget.position;

					if(absX >= 0.4){

						rotationTarget = this.nextTarget.rotation.eulerAngles;
					}

				}else{ // RightSwipe

					positionTarget = this.previousTarget.position;

					if(absX >= 0.4){

						rotationTarget = this.previousTarget.rotation.eulerAngles;
					}
				}

				positionOffset = (this.currentTarget.position - positionTarget) * absX;

			}else if(absY >= 0.1){

				if( TouchInput.IsUpSwipe(y) ){

					float diff = (this.fieldOfView - this.zoom);

					this.focuser.fieldOfView = this.fieldOfView - diff * absY;

				}else{ // DownSwipe

					positionTarget = this.altTarget.position;

					if(absY >= 0.4){

						rotationTarget = this.altTarget.rotation.eulerAngles;
					}

					positionOffset = (this.currentTarget.position - positionTarget) * absY;
				}
			}

			this.moveTo.target		= this.currentTarget.position - positionOffset;
			this.rotateAs.target	= Quaternion.Euler( rotationTarget );
		}
	}

	private void UpdateTooltip(){

		switch(this.positionIndex){

			case -1:

				this.info.text = "WELCOME TO THE DOJO\nSLIDE LEFT/RIGHT: NAVIGATE\nSLIDE UP: ZOOM\nSLIDE DOWN: TOGGLE HIRAGANA/KATAKANA";
				break;

			case 0:

				this.info.text = "IF YOU WANT TO LEAVE THE DOJO PLEASE SLIDE DOWN";
				break;

			case 1:

				this.info.text = "HIRAGANA 1 (Seion)";
				break;

			case 2:

				this.info.text = "HIRAGANA 2 (Dakuon & Handakuon)";
				break;

			case 3:

				this.info.text = "HIRAGANA 3 (Yoon-Seion, Yoon-Dakuon & Yoon-Handakuon)";
				break;

			case 4:

				this.info.text = "KATAKANA 1 (Seion)";
				break;

			case 5:

				this.info.text = "KATAKANA 2 (Dakuon & Handakuon)";
				break;

			case 6:

				this.info.text = "KATAKANA 3 (Yoon-Seion, Yoon-Dakuon & Yoon-Handakuon)";
				break;
		}
	}

	private void UpdatePositions(){

		int altTarget;

		this.UpdateTooltip();

		if(this.positionIndex < 0){

			this.currentTarget	= this.outside;

		}else{

			this.currentTarget	= this.positions[this.positionIndex];
		}

		altTarget = this.GetAlt();

		if(altTarget < 0){

			this.altTarget = this.outside;

		}else{

			this.altTarget = this.positions[altTarget];
		}

		this.previousTarget	= this.positions[ this.GetPrevious() ];
		this.nextTarget		= this.positions[ this.GetNext() ];

		this.moveTo.target		= this.currentTarget.position;
		this.rotateAs.target	= this.currentTarget.rotation;
	}

	private int GetAlt(){

		switch(this.positionIndex){

			case -1:
				return(0);

			case 0:
				return(-1);

			default:

				return(this.positions.Length - this.positionIndex);
		}
	}

	private int GetNext(){

		if(this.positionIndex == -1){

			return(this.positions.Length - 1);

		}else if(this.positionIndex + 1 < this.positions.Length){

			return(this.positionIndex + 1);

		}else if(this.loopPositions){

			return(0);
		}

		return(this.positionIndex);
	}

	private int GetPrevious(){

		if(this.positionIndex == -1){

			return(1);

		}else if(this.positionIndex > 0){

			return(this.positionIndex - 1);

		}else if(this.loopPositions){

			return(this.positions.Length - 1);
		}

		return(this.positionIndex);
	}

	public void Swipe(float x, float y, TouchData touch){

		float	absX		= Mathf.Abs(x);
		float	absY		= Mathf.Abs(y);
		bool	sideways	= absX >= absY;

		this.focuser.fieldOfView = this.fieldOfView;

		if(sideways && absX >= 0.4){

			if( TouchInput.IsLeftSwipe(x) ){

				this.positionIndex = this.GetNext();

			}else{ // RightSwipe

				this.positionIndex = this.GetPrevious();
			}

			this.observer.animation.Play();
			this.sounds.Play();

		}else if(absY >= 0.4){

			if( TouchInput.IsDownSwipe(y) ){

				this.positionIndex = this.GetAlt();

				this.observer.animation.Play();
				this.sounds.Play();
			}
		}

		this.UpdatePositions();
	}
}
