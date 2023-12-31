using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGlitch : MonoBehaviour
{
    public Material glitchMaterial;  // Das Material, das die Flicker-Textur verwendet
    
    public float flickerDuration = 0.3f;  // Die Dauer, für die die Flicker-Textur angezeigt wird

    private Renderer _fishRenderer;
    private Material _originalMaterial;
    private bool _isFlickering;

    private void Start()
    {
        _fishRenderer = GetComponent<Renderer>();
        
        Material material = _fishRenderer.material;
        _originalMaterial = material;
        
    }

    private void Update()
    {
        if (!_isFlickering)
        {
            // Starte den Flicker-Prozess in einem Coroutine
            StartCoroutine(FlickerFish());
        }
    }

    private IEnumerator FlickerFish()
    {
        List<string> images = Gallery.loadPngs();
        byte[] imageBytes = System.IO.File.ReadAllBytes(images[0]);
        Texture2D loadTexture = new Texture2D(1,1); //
        loadTexture.LoadImage(imageBytes);

        glitchMaterial.SetTexture("_PlayerImage", loadTexture);

        _isFlickering = true;

        // Zeige die Flicker-Textur an
        _fishRenderer.material = glitchMaterial;
        glitchMaterial.mainTexture = loadTexture;

        // Warte für die angegebene Dauer
        yield return new WaitForSeconds(flickerDuration);

        // Setze die ursprüngliche Textur wieder ein
        _fishRenderer.material = _originalMaterial;
        
        yield return new WaitForSeconds(2);


        _isFlickering = false;
    }
}
