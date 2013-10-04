
using UnityEngine;
using System.Collections;


public class PrefabGenerator : MonoBehaviour{

	public GameObject	prefab;
	public float		dispersionRadius	= 0f;
	public float		timeToLive			= 5f;
	public float		productionSpeed		= 10f;
	public int			max					= 100;
	public float		scaleVariation		= 1f;

	private int			generated			= 0;
	private float		time				= 0;

	void Update(){

		GameObject instance;

		this.time += Time.deltaTime;

		if(this.max > 0 && this.generated >= this.max){

			GameObject.Destroy(this);

		}else if(this.generated < this.productionSpeed * this.time){

			instance = (GameObject)GameObject.Instantiate(this.prefab, this.transform.position + (Vector3)(Random.insideUnitSphere * this.dispersionRadius), Random.rotation);

			if(scaleVariation > 1){

				float scale = Random.Range(1, this.scaleVariation);

				instance.transform.localScale = new Vector3(
					instance.transform.localScale.x * scale,
					instance.transform.localScale.y * scale,
					instance.transform.localScale.z * scale
				);
			}

			instance.transform.parent = this.transform;

			if(this.timeToLive > 0){

				GameObject.Destroy(instance, this.timeToLive);
			}

			this.generated++;
		}
	}
}
