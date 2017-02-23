
using UnityEngine;
using System.Collections;


public class SplashCollapsable : MonoBehaviour {

	public AudioSource sound;

	public int		nextScene			= -1;
	public bool		exitApplication		= false;

	public bool		destroyColliderOnCollapse	= true;
	public bool		destroyAnimationOnCollapse	= true;

	protected bool	wasHit				= false;
	protected bool	isCollapsed			= false;

	void Start(){

		this.IsKinematic(this.gameObject, true);
		this.UseGravity(this.gameObject, false);
	}

	protected void UseGravity(GameObject obj, bool useGravity){

		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody body in bodies){

			body.useGravity = useGravity;
		}
	}

	protected void IsKinematic(GameObject obj, bool isKinematic){

		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody body in bodies){

			body.isKinematic = isKinematic;
		}
	}

	protected void Explode(GameObject obj, Vector3 explosionPoint, float explosionForce, float explosionRadius){

		Rigidbody[] bodies = obj.GetComponentsInChildren<Rigidbody>();

		foreach(Rigidbody body in bodies){

			body.AddExplosionForce(explosionForce, explosionPoint, explosionRadius);
		}
	}

	public virtual void Collapse(Vector3? hitPoint, float explosionForce, float explosionRadius){

		this.isCollapsed = true;

		this.IsKinematic(this.gameObject, false);
		this.UseGravity(this.gameObject, true);

		if(hitPoint.HasValue && explosionForce > 0){

			this.Explode(this.gameObject, hitPoint.Value, explosionForce, explosionRadius);
		}

		if(this.destroyColliderOnCollapse){

			Object.Destroy(this.gameObject.GetComponent<Collider>());
		}

		if(this.destroyAnimationOnCollapse){

			Object.Destroy(this.gameObject.GetComponent<Animation>());
		}

		Object.Destroy(this);
	}

	public virtual void Hit(Vector3? hitPoint){

		if(this.sound != null){

			this.sound.Play();
		}

		if(!this.isCollapsed){

			this.wasHit = true;
		}
	}
}
