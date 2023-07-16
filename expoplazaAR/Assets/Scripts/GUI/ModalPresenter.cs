using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
	[RequireComponent(typeof(UIDocument))]
	public class ModalPresenter : MonoBehaviour
	{
		[SerializeField] private AudioClip clip;

		private AudioSource audioSource;

		private VisualElement modal;
		private Label titleLabel;
		private Label textLabel;

		private Action<bool> callback;

		private void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}

		private void OnEnable()
		{
			modal = GetComponent<UIDocument>().rootVisualElement.Q("modal");
			titleLabel = modal.Q<Label>("title");
			textLabel = modal.Q<Label>("text");

			modal.Query<Button>(className: "button").ForEach(btn => btn.clicked += Hide);
			modal.Q<Button>("yesButton").clicked += () => callback(true);
			modal.Q<Button>("noButton").clicked += () => callback(false);
		}

		public void ShowModal(string title, string text, Action<bool> callback)
		{
			titleLabel.text = title;
			textLabel.text = text;
			this.callback = callback;

			Show();
		}

		private void Show()
		{
			audioSource.clip = clip;
			audioSource.pitch = 1;
			audioSource.PlayDelayed(0.3f);
			modal.RemoveFromClassList("modal--hidden");
		}

		private void Hide()
		{
			modal.AddToClassList("modal--hidden");
		}
	}
}