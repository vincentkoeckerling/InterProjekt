using System.Collections;
using System.IO;
using UnityEngine;
using Unity.XR.CoreUtils;

namespace Helper
{
	public class CameraHelper
	{

		private static string GetFrontCamName()
		{
			foreach (WebCamDevice camDevice in WebCamTexture.devices)
				if (camDevice.isFrontFacing)
					return camDevice.name;

			return "";
		}

		public static IEnumerator TakePhoto()  // Start this Coroutine on some button click
		{
			var camPos = Camera.main.transform.position;
			var camForward = Camera.main.transform.forward;

			WebCamTexture webCamTexture = new(GetFrontCamName());

			webCamTexture.Play();
			yield return new WaitForSeconds(5);

			Texture2D photo = new(webCamTexture.width, webCamTexture.height, TextureFormat.ARGB32, false);
			photo.SetPixels(webCamTexture.GetPixels());
			photo.Apply();

			byte[] bytes = photo.EncodeToPNG();
			string filename =
				 $"{Application.productName}_Capture{{0}}_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
			NativeGallery.SaveImageToGallery(bytes, Application.productName + " Captures", filename);


			DirectoryInfo directoryInfo = Directory.CreateDirectory(Application.persistentDataPath).CreateSubdirectory("ARFishing");
			string destination = directoryInfo.FullName + filename;
			Debug.Log("save destination: " + destination);


			FileStream file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);
			file.Write(bytes);
			file.Close();

			webCamTexture.Stop();
			yield return new WaitForSeconds(0.1f);
			Debug.Log(Camera.main.transform.position);
			var xrorigin = GameObject.FindWithTag("XROrigin").GetComponent<XROrigin>();
			xrorigin.MoveCameraToWorldLocation(camPos);
			xrorigin.MatchOriginUpCameraForward(xrorigin.transform.up, camForward);
			// Camera.main.transform.position = camPos;
		}
	}
}