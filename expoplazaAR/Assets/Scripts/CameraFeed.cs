
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class CameraFeed : MonoBehaviour
{
	WebCamTexture webCamTexture;
	public UnityEngine.UI.Button button2;
	public RawImage rawImage;
	
	void Start()
	{
		Debug.Log("CameraFeed initialzing...");
		button2.onClick.AddListener(Image);

		string frontCamName = "";
		
		WebCamDevice[] webCamDevices = WebCamTexture.devices;
		foreach(WebCamDevice camDevice in webCamDevices){
			if (camDevice.isFrontFacing)
			{
				frontCamName = camDevice.name;
			}
		}
		Debug.Log("Identified front camera: " + frontCamName);
		
		
		webCamTexture = new WebCamTexture(frontCamName);
		rawImage.texture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
		webCamTexture.Play();
	}

	public void Image()
	{
		StartCoroutine(TakePhoto());
	}
	
	public IEnumerator TakePhoto()  // Start this Coroutine on some button click
	{
		Debug.Log("TakePhoto()");
		
		// NOTE - you almost certainly have to do this here:

		yield return new WaitForEndOfFrame();

		// it's a rare case where the Unity doco is pretty clear,
		// http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
		// be sure to scroll down to the SECOND long example on that doco page 

		Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
		photo.SetPixels(webCamTexture.GetPixels());
		photo.Apply();

		//Encode to a PNG
		byte[] bytes = photo.EncodeToPNG();
		//Write out the PNG. Of course you have to substitute your_path for something sensible

		// Save the screenshot to Gallery/Photos
		string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
		Debug.Log("filename: " + name);
		Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(bytes, Application.productName + " Captures", name));
		
		
		string destination = Application.persistentDataPath + name;
		FileStream file;
 
		if(File.Exists(destination)) file = File.OpenWrite(destination);
		else file = File.Create(destination);
		file.Write(bytes);
		file.Close();
		Debug.Log("file saved at " + destination);
	}
}
