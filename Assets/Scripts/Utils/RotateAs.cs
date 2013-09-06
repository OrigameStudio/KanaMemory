
using UnityEngine;
using System.Collections;


public class RotateAs : MonoBehaviour{

	public Quaternion	target;
	public float		speed;
	public bool			spherical;

	void Update(){

		if(spherical){

			this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, this.target, this.speed);

		}else{

			this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, this.target, this.speed);
		}
	}

	public static RotateAs Add(GameObject obj, Quaternion target, float speed = 0.1f, bool spherical = false){

		RotateAs that = obj.AddComponent<RotateAs>();

		that.target		= target;
		that.speed		= speed;
		that.spherical	= spherical;

		return(that);
	}
}
