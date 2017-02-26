
using UnityEngine;
using System.Collections;


public class GameAtrezzoControl : MonoBehaviour{

	public float explosionForce		= 100f;
	public float explosionRadius	= 1f;

	void Update(){

		if( Input.GetMouseButtonDown(0) ){

			Ray rayo;
			RaycastHit info;

			rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

			if( Physics.Raycast(rayo, out info) ){

				if( info.collider.gameObject.tag == "ATREZZO" ){

                    var rigidbody = info.rigidbody;

                    if(rigidbody == null){
                        rigidbody = info.transform.parent.gameObject.GetComponent<Rigidbody>();
                    }

                    rigidbody.AddExplosionForce(this.explosionForce, info.point, this.explosionRadius);
				}
			}
		}

	}

}
