using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour{

	public Vector3	target;
	public float	speed;
	public bool		spherical;

	void Update(){

		if(spherical){

			this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, this.target, this.speed);

		}else{

			this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, this.target, this.speed);
		}
	}

	public static MoveTo Add(GameObject obj, Vector3 target, float speed = 0.1f, bool spherical = false){

		MoveTo that = obj.AddComponent<MoveTo>();

		that.target		= target;
		that.speed		= speed;
		that.spherical	= spherical;

		return(that);
	}
}
