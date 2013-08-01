using UnityEngine;
using System.Collections;

public class SplashScreenCollapsable : MonoBehaviour {

	public int	nextScene = -1;
	public bool	exitApplication = false;

	void Start(){

		this.IsKinematic(this.gameObject, true);
		this.UseGravity(this.gameObject, false);
	}

	private void UseGravity(GameObject obj, bool useGravity){

		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody body in bodies){

			body.useGravity = useGravity;
		}
	}

	private void IsKinematic(GameObject obj, bool isKinematic){

		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody body in bodies){

			body.isKinematic = isKinematic;
		}
	}

	private void Explode(GameObject obj, Vector3 explosionPoint, float explosionForce, float explosionRadius){

		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody body in bodies){

			body.AddExplosionForce(explosionForce, explosionPoint, explosionRadius);
		}
	}

	public virtual void Collapse(Vector3? hitPoint, float explosionForce, float explosionRadius){

		this.IsKinematic(this.gameObject, false);
		this.UseGravity(this.gameObject, true);

		if(hitPoint.HasValue && explosionForce > 0){

			this.Explode(this.gameObject, hitPoint.Value, explosionForce, explosionRadius);
		}

		Object.Destroy(this.gameObject.animation);
		Object.Destroy(this.gameObject.collider);
		Object.Destroy(this);
	}

	public virtual void Hit(Vector3? hitPoint){
		
		/* ... */
	}
}
