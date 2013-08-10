using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	public Transform target;

	void Update(){

		if(this.target != null){

			this.gameObject.transform.LookAt(this.target);
		}
	}

	public static LookAt Add(GameObject obj, Transform target = null){

		LookAt that = obj.AddComponent<LookAt>();

		that.target = target;

		return(that);
	}
}
