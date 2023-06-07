using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Cam : MonoBehaviour
{
    WebCamTexture webCamTexture;
    
        void Start() 
        {
            webCamTexture = new WebCamTexture();
            GetComponent<Renderer>().material.mainTexture = webCamTexture; //Add Mesh Renderer to the GameObject to which this script is attached to
            webCamTexture.Play();
            
            StartCoroutine(TakePhoto());
        }

    IEnumerator TakePhoto()  // Start this Coroutine on some button click
        {
    
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
            Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(bytes, Application.productName + " Captures", name));
        }
}
