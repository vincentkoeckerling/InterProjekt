using System.Collections;
using System.IO;
using UnityEngine;

namespace Helper
{
    public class CameraHelper
    {
        
        private static string GetFrontCamName()
        {
            foreach(WebCamDevice camDevice in WebCamTexture.devices)
                if (camDevice.isFrontFacing)
                    return camDevice.name;

            return "";
        }
        
        public static IEnumerator TakePhoto()  // Start this Coroutine on some button click
        {
            WebCamTexture webCamTexture = new(GetFrontCamName());

            webCamTexture.Play();
            yield return new WaitForSeconds(5);

            Texture2D photo = new(webCamTexture.width, webCamTexture.height);
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
        }
    }
}