
using UnityEngine;
using System.Collections;


public class HelpControl : MonoBehaviour, SwipeListener{

	public SwipeControl swipe;

	public Transform[] positions;

	public int positionIndex = 0;

	public Camera observer;

	public int escapeScene = 1;

	public GUIText info;

	public MoveTo		moveTo;
	public RotateAs		rotateAs;
	private Transform currentTarget;
	public Transform previousTarget;
	public Transform nextTarget;

	void Start(){

		this.swipe.AddListener(this);

		this.moveTo		= MoveTo.Add(this.observer.gameObject, this.observer.transform.position, 0.1f);
		this.rotateAs	= RotateAs.Add(this.observer.gameObject, this.observer.transform.rotation, 0.1f);

		this.UpdatePositions();
	}

	void Update(){

		this.info.text = "<" + positionIndex + ">" + " (" + this.moveTo.target.x + ")\n" + "currentTarget: " + currentTarget.transform.position.x + "\n" + "previousTarget: " + previousTarget.transform.position.x + "\n" + "nextTarget: " + nextTarget.transform.position.x;

		if( Input.GetKey(KeyCode.Escape) ){

			Application.LoadLevel(this.escapeScene);
		}

		if( this.swipe.isSwiping ){

			float		x				= this.swipe.total.x;
			Vector3		positionTarget	= this.currentTarget.position;
			Vector3		rotationTarget	= this.currentTarget.rotation.eulerAngles;
			Vector3		positionOffset;
			Vector3		rotationOffset;

			if( TouchInput.IsLeftSwipe(x) ){

				positionTarget = this.nextTarget.position;
				rotationTarget = this.nextTarget.rotation.eulerAngles;

			}else if( TouchInput.IsRightSwipe(x) ){

				positionTarget = this.previousTarget.position;
				rotationTarget = this.previousTarget.rotation.eulerAngles;
			}

			positionOffset = (this.currentTarget.position - positionTarget) * Mathf.Abs(x);
			rotationOffset = (this.currentTarget.rotation.eulerAngles - rotationTarget) * Mathf.Abs(x);

			this.moveTo.target		= this.currentTarget.position - positionOffset;
			//this.rotateAs.target	= Quaternion.Euler( this.currentTarget.rotation.eulerAngles - rotationOffset );
			this.rotateAs.target	= Quaternion.Euler( rotationTarget );
		}
	}

	private void UpdatePositions(){

		this.currentTarget	= this.positions[this.positionIndex];
		this.previousTarget	= ( this.positionIndex == 0 ? this.currentTarget : this.positions[this.positionIndex - 1] );
		this.nextTarget		= ( this.positionIndex == this.positions.Length - 1 ? this.currentTarget : this.positions[this.positionIndex + 1] );

		this.moveTo.target		= this.currentTarget.position;
		this.rotateAs.target	= this.currentTarget.rotation;
	}

	private void Next(){

		if(this.positionIndex + 1 < this.positions.Length){

			this.positionIndex++;
		}
	}

	private void Previous(){

		if(this.positionIndex > 0){

			this.positionIndex--;
		}
	}

	public void Swipe(float x, float y, TouchData touch){

		if( Mathf.Abs(x) < 0.4 ){

			/* ... */

		}else if( TouchInput.IsLeftSwipe(x) ){

			this.Next();

		}else if( TouchInput.IsRightSwipe(x) ){

			this.Previous();
		}

		this.UpdatePositions();
	}
}
