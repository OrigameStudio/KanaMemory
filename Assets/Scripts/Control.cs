using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public Card card1 = null;
	public Card card2 = null;

	// Update is called once per frame
	void Update () {

		//if ( Input.GetKeyDown (KeyCode.Alpha0)) pepino.Flip();

		if ( Input.GetMouseButtonDown (0)){

			Ray rayo;
			RaycastHit info;

			rayo = Camera.main.ScreenPointToRay (Input.mousePosition);

			if ( Physics.Raycast (rayo, out info)) {

				Card card;
				int count;

				count = this.FlippedCardCount ();
				card = info.collider.gameObject.GetComponent<Card>();

				if ( count == 2){
					card1.Flip();
					card2.Flip();

					card1 = card;
					card2 = null;
				}

				if ( count == 1){

					card2 = card;
				}

				if ( count == 0){

					card1 = card;
				}

				if(card != null){

					card.Flip();
				}


				if ( count == 1){

					print ("carta 1: " + card1.cardName);
					print ("carta 2: " + card2.cardName);
					print ("equivalentes? " + (card1.character == card2.character ? "SI, CORRECTO" : "NOOOOO CHIGAUMASU"));
				}
			}

			//print ("ojete");
			//pepino.Flip();
		}
	}

	private int FlippedCardCount(){

		if ( card1 == null && card2 == null ) return (0);

		if ( card1 != null && card2 != null ) return (2);

		return (1);

	}
}
