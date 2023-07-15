using Helper;
using UnityEngine;

public class CameraFeed : MonoBehaviour
{
	public UnityEngine.UI.Button button2;

	private void Start()
	{
		if(button2 != null)
			button2.onClick.AddListener(Image);
	}


	private void Image()
	{
		StartCoroutine(CameraHelper.TakePhoto());
	}
}
