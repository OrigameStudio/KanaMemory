
using UnityEngine;
using System.Collections;


public class HelpExitTrigger : MonoBehaviour{

	public HelpControl control;

	public bool isInDojo = false;

	void OnTriggerEnter(Collider other){

		if(this.isInDojo){

			this.control.Exited();
		}
	}

	void OnTriggerExit(Collider other){

		this.isInDojo = true;
	}
}
