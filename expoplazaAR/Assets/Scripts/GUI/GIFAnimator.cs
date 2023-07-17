using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
	public class GIFAnimator : MonoBehaviour
	{
		[SerializeField] private string elementName;
		[SerializeField] private string imagePath;

		private VisualElement target;

		private List<Texture2D> frames = new List<Texture2D>();
		private List<float> delays = new List<float>();

		private int curFrame = 0;
		private float time = 0f;

		private void Start()
		{
			var path = Path.Combine(Application.streamingAssetsPath, imagePath);
			var bytes = File.ReadAllBytes(path);

			using (var decoder = new MG.GIF.Decoder(bytes))
			{
				var img = decoder.NextImage();

				while (img != null)
				{
					frames.Add(img.CreateTexture());
					delays.Add(img.Delay / 1000.0f);
					img = decoder.NextImage();
				}
			}
		}

		private void OnEnable()
		{
			target = GetComponent<UIDocument>().rootVisualElement.Q(elementName);
		}

		private void Update()
		{
			if (target == null) return;
			if (frames.Count == 0) return;

			time += Time.deltaTime;

			if (time >= delays[curFrame])
			{
				curFrame = (curFrame + 1) % frames.Count;
				time = 0f;

				target.style.backgroundImage = frames[curFrame];
			}
		}
	}
}
