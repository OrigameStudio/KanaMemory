
using UnityEngine;
using System.Collections;


public class HelpControl : MonoBehaviour{

	public GameObject Welcome;
	public GameObject Navigation;
	public GameObject Goodbye;
	private bool isLearning;

	public Transform[] zoomedInPositions;
	public Transform[] zoomedOutPositions;

	public Transform enterPosition;
	public Transform exitPosition;
	public Transform outsidePosition;

	public float moveSpeed		= 0.1f;
	public float rotateSpeed	= 0.01f;

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

	public AudioSource sounds;
	public AudioSource voice;

	//public GUIText info;

	private	MoveTo		moveTo;
	private	RotateAs	rotateAs;

	private	Transform	currentTarget;

	void Start(){

		this.moveTo		= MoveTo.Add(this.observer.gameObject, this.enterPosition.transform.position, this.moveSpeed);
		this.rotateAs	= RotateAs.Add(this.observer.gameObject, this.enterPosition.transform.rotation, this.rotateSpeed, true);

		this.Welcome.SetActive(true);
		this.Navigation.SetActive(false);
		this.Goodbye.SetActive(false);
	}

	void Update(){

		if( Input.GetKey(KeyCode.Escape) ){

			this.ConfirmExit();

		}else if( this.isLearning && Input.GetMouseButtonDown(0) ){

			Ray ray;
			RaycastHit info;

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if( Physics.Raycast(ray, out info) ){

				Card card = info.collider.gameObject.GetComponent<Card>();

				if(card != null){

					// Read aloud
					this.voice.PlayOneShot(card.sound);
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

	public void Next(){

		if(this.positionIndex == 5){

			this.positionIndex = 0;

		}else{

			this.positionIndex++;
		}

		this.observer.animation.Play();
		this.sounds.Play();

		this.UpdateCurrentTarget();
	}

	public void Previous(){

		if(this.positionIndex == 0){

			this.positionIndex = 5;

		}else{

			this.positionIndex--;
		}

		this.observer.animation.Play();
		this.sounds.Play();

		this.UpdateCurrentTarget();
	}

	public void Switch(){

		if(this.positionIndex < 3){

			this.positionIndex += 3;

			this.switchIcon.texture = this.switchToHiraganaTexture;

		}else{

			this.positionIndex -= 3;

			this.switchIcon.texture = this.switchToKatakanaTexture;
		}

		this.observer.animation.Play();
		this.sounds.Play();

		this.UpdateCurrentTarget();
	}

	public void SwitchToHiragana(){

		this.Welcome.SetActive(false);
		this.Navigation.SetActive(true);
		this.isLearning = true;

		this.switchIcon.texture = this.switchToKatakanaTexture;

		this.isZoomed = false;

		this.positionIndex = this.firstHiraganaIndex;

		this.observer.animation.Play();
		this.sounds.Play();

		this.UpdateCurrentTarget();
	}

	public void SwitchToKatakana(){

		this.Welcome.SetActive(false);
		this.Navigation.SetActive(true);
		this.isLearning = true;

		this.switchIcon.texture = this.switchToHiraganaTexture;

		this.isZoomed = false;

		this.positionIndex = this.firstKatakanaIndex;

		this.observer.animation.Play();
		this.sounds.Play();

		this.UpdateCurrentTarget();
	}

	public void ConfirmExit(){

		this.Welcome.SetActive(false);
		this.Navigation.SetActive(false);
		this.Goodbye.SetActive(true);
		this.isLearning = false;

		this.moveTo.target		= this.exitPosition.transform.position;
		this.rotateAs.target	= this.exitPosition.transform.rotation;

		this.observer.animation.Play();
		this.sounds.Play();
	}

	public void Stay(){

		this.Goodbye.SetActive(false);
		this.Navigation.SetActive(false);
		this.Welcome.SetActive(true);
		this.isLearning = false;

		this.moveTo.target		= this.enterPosition.transform.position;
		this.rotateAs.target	= this.enterPosition.transform.rotation;

		this.observer.animation.Play();
		this.sounds.Play();
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

	public void Exit(){

		this.Goodbye.SetActive(false);

		this.moveTo.target		= this.outsidePosition.transform.position;
		this.rotateAs.target	= this.outsidePosition.transform.rotation;

		//yield return( new WaitForSeconds(1) );
		//Application.LoadLevel(0); //this.escapeScene);
	}

}
