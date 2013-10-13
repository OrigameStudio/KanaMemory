
using UnityEngine;
using System.Collections;


public class Zoomer : MonoBehaviour{
	
	public Camera			cam;
	public Transform		target;
	public MonoBehaviour	script;
	public float			zoomedInFieldOfView		= 15f;
	public float			zoomedOutFieldOfView	= 60f;
	public float			speed					= 0.1f;
	public bool				isZoomingIn				= false;

	void Update(){
		
		float fieldOfView = this.zoomedOutFieldOfView;
		
		if(this.isZoomingIn){
			
			fieldOfView = this.zoomedInFieldOfView;
			
			if( this.cam.fieldOfView < this.zoomedInFieldOfView + 2){

				this.ZoomOut();
			}
		}
		
		// Change field of view smoothly
		this.cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, fieldOfView, Time.deltaTime * this.speed);
		
		// Look at target smoothly
		if(this.target != null){

			Quaternion rotation = Quaternion.LookRotation(this.target.position - this.gameObject.transform.position);

			this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, rotation, Time.deltaTime * this.speed);
		}
	}
	
	public void ZoomOut(){
		
		this.isZoomingIn = false;
		
		this.target = null;

		this.script.enabled = true;
	}
	
	public void ZoomIn(Transform target){
		
		this.isZoomingIn = true;
		
		this.target = target;
		
		this.script.enabled = false;
	}
	
	public static Zoomer Add(GameObject obj, Camera cam, float zoomIn, float zoomOut, float speed, MonoBehaviour script = null){

		Zoomer that;

		that = obj.AddComponent<Zoomer>();

		that.cam					= cam;
		that.zoomedInFieldOfView	= zoomIn;
		that.zoomedOutFieldOfView	= zoomOut;
		that.speed					= speed;
		that.script					= script;

		return(that);
	}
}
