
using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour{

	public bool		captureNow	= false;
	public int		progress	= 0;

	public string	path;
	public string	file		= "screenshot";
	public string	separator	= "-";
	public KeyCode	key			= KeyCode.S;
	public string[]	sizes		= { "phone", "7inch-tablet", "10inch-tablet" };

	private float	timeScale;
	private int		counter		= 0;
	private int		captured	= 0;
	private bool 	isCapturing	= false;
	private string	waitForFile	= null;

	void Start(){

		this.captureNow = false;

		GameObject.DontDestroyOnLoad(this.gameObject);
	}

	void LateUpdate(){

		if(!UnityEditor.EditorApplication.isPaused){

			if(this.isCapturing){

				this.Capture();

			}else if( Input.GetKeyDown(this.key) ){

				this.StartCapturing();
			}
		}
	}

	void OnGUI(){

		if(UnityEditor.EditorApplication.isPaused){

			if(this.isCapturing){

				this.Capture();

			}else if(this.captureNow){

				this.StartCapturing();
				//this.StartCoroutine( this.CaptureAll() );
			}
		}
	}

	/*
	private IEnumerator CaptureAll(){

		print("OJETE");

		this.StartCapturing();

		while(this.isCapturing){

			print("CALOR...");

			this.Capture();

			yield return( new WaitForSeconds(1f) );
		}

		this.captureNow = false;
	}
	*/

	private void Capture(){

		this.progress++;

		if(this.captured >= this.sizes.Length){

			this.StopCapturing();

		}else if(this.waitForFile != null){

			if( System.IO.File.Exists(this.waitForFile) ){

				Debug.Log("Screenshot captured: " + this.waitForFile);

				this.waitForFile = null;
			}

		}else{

			int		sizeValue	= this.captured + 1;
			string	sizeName	= this.sizes[this.captured];
			string	filePath	= this.GetFilePath(sizeName);

			Debug.Log("Capturing screenshot to: " + filePath + " (size: " + sizeValue + ")...");

			Application.CaptureScreenshot(filePath, sizeValue);

			this.waitForFile = filePath;

			this.captured++;
		}
	}

	private void StartCapturing(){

		this.counter++;
		this.isCapturing	= true;
		this.captured		= 0;
		this.timeScale		= Time.timeScale;
		Time.timeScale		= 0.0f;
		this.progress		= 0;
	}

	private void StopCapturing(){

		this.isCapturing	= false;
		Time.timeScale		= this.timeScale;
		this.captureNow		= false;
	}

	private string GetFilePath(string sizeName){

		string tmp;

		tmp = Application.loadedLevelName + this.separator + this.file + this.separator + this.counter + this.separator + sizeName + ".png";

		return( System.IO.Path.Combine(this.path, tmp) );
	}

}
