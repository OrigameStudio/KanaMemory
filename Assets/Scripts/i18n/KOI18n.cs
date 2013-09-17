
using UnityEngine;
using System.Collections;


public class KOI18n : MonoBehaviour{

	public GameObject placeholder;

	void Start(){

		GameObject.Destroy(this.placeholder);

		GameObject.Instantiate(MemoryGame.GetInstance().language.Mesh_KO);
	}

}
