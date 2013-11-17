
using UnityEngine;
using System.Collections;


public class GameAtrezzoTeaCup : MonoBehaviour{

	public GameObject liquidPrefab;
	public Transform liquidTransform;

	void Start(){

		GameObject liquid = (GameObject)GameObject.Instantiate(this.liquidPrefab, this.liquidTransform.position, this.liquidTransform.rotation);

		liquid.transform.parent = this.liquidTransform;
	}

}
