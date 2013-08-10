
using UnityEngine;
using System.Collections.Generic;


public class Card : MonoBehaviour{

	public string cardName;
	public Alphabet alphabet;
	public Character character;

	private bool isFaceDown = true;
	private AnimationClip flip1;
	private AnimationClip flip2;
	private AnimationClip wrong;
	private AnimationClip right;

	private Transform targetPosition;
	private Transform targetObserver;

	// Use this for initialization
	void Start(){

		this.flip1 = this.animation.GetClip("Card@Flip1");
		this.flip2 = this.animation.GetClip("Card@Flip2");
		this.wrong = this.animation.GetClip("Card@Wrong");
		this.right = this.animation.GetClip("Card@Right");
	}

	public void Reset(Card that){

		GameObject	thisBody;
		GameObject	thatBody;

		this.cardName	= that.cardName;
		this.alphabet	= that.alphabet;
		this.character	= that.character;

		thisBody = this.GetBody();
		thatBody = that.GetBody();

		//thisBody.renderer.sharedMaterials = thatBody.renderer.sharedMaterials;

		//thisBody = (GameObject)GameObject.Instantiate(thatBody);

		thatBody.transform.parent	= thisBody.transform.parent;
		thatBody.transform.position	= thisBody.transform.position;
		thatBody.transform.rotation	= thisBody.transform.rotation;
		thatBody.transform.localScale = thisBody.transform.localScale;

		GameObject.Destroy(thisBody);
	}

	private GameObject GetBody(){

		Transform body;

		body = this.transform.FindChild("Body");

		return(body == null ? null : body.gameObject);
	}

	private void PlayNow(AnimationClip clip){

		if(this.animation.isPlaying){

			this.animation.Stop();
		}

		this.animation.clip = clip;

		this.animation.Play();
	}

	private void PlayLater(AnimationClip clip){

		this.animation.PlayQueued(clip.name);
	}

	public void FlipFaceUp(){

		if(this.isFaceDown){

			this.PlayNow(this.flip1);

			this.isFaceDown = false;
		}
	}

	public void FlipFaceDown(){

		if(!this.isFaceDown){

			this.PlayNow(this.flip2);

			this.isFaceDown = true;
		}
	}

	public void Failure(){

		this.PlayLater(this.wrong);
	}

	public void Success(Transform targetPosition, Transform targetObserver){

		this.PlayLater(this.right);

		MoveTo.Add(this.gameObject, targetPosition, 0.075f);
		LookAt.Add(this.gameObject, targetObserver);

		GameObject.Destroy(this.gameObject, 2.5f);
	}

	public bool Matches(Card that){

		//return( Random.Range(0, 2) == 0 );

		return( this.character == that.character);
	}

	public static Card FindMatchingCard(List<Card> cards, Card card){

		foreach(Card matchingCard in cards){

			if( matchingCard.Matches(card) ){

				return(matchingCard);
			}
		}

		return(null);
	}
}
