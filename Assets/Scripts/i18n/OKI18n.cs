
using UnityEngine;
using System.Collections;


public class OKI18n : MonoBehaviour{

	public GameObject placeholder;

	void Start(){

		GameObject.Destroy(this.placeholder);

		GameObject.Instantiate(MemoryGame.GetInstance().language.Mesh_OK);
	}

}
