using UnityEngine;

public class DoubleCamera : MonoBehaviour
{
	

    public Renderer frontOutput;
    public Renderer backOutput;

	// Start is called before the first frame update
	void Start()
	{
        var frontCameraName = WebCamTexture.devices[0].name;
        var backCameraName = WebCamTexture.devices[1].name;

		// var frontCamera = new WebCamTexture(frontCameraName.name);
		// frontOutput.material.mainTexture = frontCamera; 
        // frontCamera.Play();

        var backCamera = new WebCamTexture(backCameraName);
		backOutput.material.mainTexture = backCamera;
		backCamera.Play();

        var frontCamera = new WebCamTexture(frontCameraName);
		frontOutput.material.mainTexture = frontCamera; 
        frontCamera.Play();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
