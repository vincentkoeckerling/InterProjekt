using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class Gallery : MonoBehaviour
{
    List<string> images = new();
    int currentImageIndex = 0;
    public RawImage rawImage;
    
    public UnityEngine.UI.Button button2;
    
    public MeshRenderer meshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        loadPngs();
        button2.onClick.AddListener(showNextImage);
        showImage();

    }
    
    private void showImage()
    {
        Debug.Log("loading index " + currentImageIndex + " of " + images.Count);
        byte[] imageBytes = System.IO.File.ReadAllBytes(images[currentImageIndex]);
        Debug.Log("imageBytes: " + imageBytes.Length);

        Texture2D loadTexture = new Texture2D(1,1); //mock size 1x1
        loadTexture.LoadImage(imageBytes);
        
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

    private void loadPngs()
    {
        string destination = Application.persistentDataPath;
        destination = System.IO.Directory.GetParent(destination).FullName;
        Debug.Log("destination: " + destination); 
        
        // all pngs
        images = System.IO.Directory.GetFiles(destination, "*.png").ToList();
        Debug.Log(images.Count + " pngs found");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
