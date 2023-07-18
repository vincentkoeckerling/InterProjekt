using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class Gallery : MonoBehaviour
{
    List<string> images = new();
    int currentImageIndex = -1;
    public RawImage rawImage;
    public int imageIndex = 0;
    
    public UnityEngine.UI.Button button2;
    
    public MeshRenderer meshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        images = loadPngs();
        if(button2 != null)
            button2.onClick.AddListener(showNextImage);

        for (int i = 0; i <= imageIndex; i++)
        {
            showNextImage();
        }

    }
    
    private void showImage()
    {
        Debug.Log("loading index " + currentImageIndex + " of " + images.Count);
        byte[] imageBytes = System.IO.File.ReadAllBytes(images[currentImageIndex]);

        Texture2D loadTexture = new Texture2D(1,1); //mock size 1x1
        loadTexture.LoadImage(imageBytes);
        
        if(rawImage != null)
            rawImage.texture = loadTexture;
        
        meshRenderer.material.mainTexture = loadTexture;
    }

    private void showNextImage()
    {
        if (currentImageIndex == (images.Count - 1))
            currentImageIndex = 0;
        else currentImageIndex++;
        
        showImage();
    }

    public static List<string> loadPngs()
    {
        DirectoryInfo directoryInfo = Directory.CreateDirectory(Application.persistentDataPath);
        string destination = directoryInfo.FullName;
        Debug.Log("read destination: " + destination); 
        
        // all pngs
        List<string> images = System.IO.Directory.GetFiles(destination, "*.png").ToList();
        Debug.Log(images.Count + " pngs found");

        return images;
    }
}
