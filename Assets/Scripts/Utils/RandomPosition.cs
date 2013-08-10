
using UnityEngine;
using System.Collections;


public class RandomPosition : MonoBehaviour{

	public Transform[] positions;

	void Start(){

		int count;
		int index;

		count = this.positions.Length;

		if(count > 0){

			index = Random.Range(0, count);

			this.gameObject.transform.position = this.positions[index].position;
			this.gameObject.transform.rotation = this.positions[index].rotation;
		}

		GameObject.Destroy(this);

	}

}
