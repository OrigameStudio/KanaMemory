using UnityEngine;
using System.Collections;

public class SplashScreenCollapsable : MonoBehaviour {
	
	void Start(){
		
		this.UseGravity(this.gameObject, false);
	}
	
	private void UseGravity(GameObject obj, bool useGravity){
		
		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();
		
		foreach(Rigidbody body in bodies){
			
			body.useGravity = useGravity;
			body.isKinematic = !useGravity;
		}
	}
	
	private void Explode(GameObject obj, Vector3 explosionPoint, float explosionForce, float explosionRadius){
		
		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();
		
		foreach(Rigidbody body in bodies){
			
			body.AddExplosionForce(explosionForce, explosionPoint, explosionRadius);
		}
	}
	
	public void Collapse(Vector3? hitPoint, float explosionForce, float explosionRadius){
		
		this.UseGravity(this.gameObject, true);
		
		if(hitPoint.HasValue && explosionForce > 0){
	
			this.Explode(this.gameObject, hitPoint.Value, explosionForce, explosionRadius);
		}
		
		Object.Destroy(this.gameObject.animation);
		Object.Destroy(this.gameObject.collider);
		Object.Destroy(this);
	}
	
}
