using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour{

	public Transform	target;
	public float		speed;
	public bool			spherical;

	void Update(){

		if(this.target != null){

			if(spherical){

				this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, this.target.position, this.speed);

			}else{

				this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, this.target.position, this.speed);
			}
		}
	}

	public static MoveTo Add(GameObject obj, Transform target = null, float speed = 0.1f, bool spherical = false){

		MoveTo that = obj.AddComponent<MoveTo>();

		that.target		= target;
		that.speed		= speed;
		that.spherical	= spherical;

		return(that);
	}
}
