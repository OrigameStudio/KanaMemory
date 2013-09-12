
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

					info.rigidbody.AddExplosionForce(this.explosionForce, info.point, this.explosionRadius);
				}
			}
		}

	}

}
