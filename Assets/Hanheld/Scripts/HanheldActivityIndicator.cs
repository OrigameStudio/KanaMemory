
using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class HanheldActivityIndicator : MonoBehaviour {

	public bool show = false;

#if UNITY_EDITOR

	public Texture dummyTexture;

	public Rect dummyPosition = new Rect(8, 8, 72, 72);

#endif

#if UNITY_ANDROID || UNITY_EDITOR

	public AndroidActivityIndicatorStyle androidActivityIndicator = AndroidActivityIndicatorStyle.DontShow;

#endif

#if UNITY_IPHONE || UNITY_EDITOR

	public iOSActivityIndicatorStyle iOsActivityIndicator = iOSActivityIndicatorStyle.DontShow;

#endif

	void Start(){

		if(this.show){

			this.Show();
		}
	}

	private IEnumerator StartActivityIndicator(){

#if UNITY_ANDROID

		Handheld.SetActivityIndicatorStyle(this.androidActivityIndicator);
		Handheld.StartActivityIndicator();

#elif UNITY_IPHONE

		Handheld.SetActivityIndicatorStyle(this.iOsActivityIndicator);
		Handheld.StartActivityIndicator();

#endif

		yield return new WaitForSeconds(0);
	}

	public void Show(){

		this.show = true;

		this.StartCoroutine( this.StartActivityIndicator() );
	}

	public void Hide(){

		Handheld.StopActivityIndicator();

		this.show = false;
	}

#if UNITY_EDITOR

	void OnGUI(){

		if(this.show){

			GUI.depth = int.MaxValue;
			GUI.DrawTexture(this.dummyPosition, this.dummyTexture);
		}
	}

#endif

	void OnDestroy(){

		if(this.show){

			this.Hide();
		}
	}

}
