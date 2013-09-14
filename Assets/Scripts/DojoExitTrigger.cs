
using UnityEngine;
using System.Collections;


public class DojoExitTrigger : MonoBehaviour{

	public DojoControl control;

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
