using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
	[RequireComponent(typeof(UIDocument))]
	public class NotificationPresenter : MonoBehaviour
	{
		[SerializeField] private AudioClip clip;

		private AudioSource audioSource;

		private VisualElement notification;
		private Label titleLabel;
		private Label textLabel;

		private Coroutine coroutine;

		private bool IsNotificationShown => !notification.ClassListContains("notification-hidden");

		private void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}

		private void OnEnable()
		{
			notification = GetComponent<UIDocument>().rootVisualElement.Q("notification");
			titleLabel = notification.Q<Label>("title");
			textLabel = notification.Q<Label>("text");

			notification.Q<Button>("dismissButton").clicked += Hide;
		}

		public void ShowNotification(string title, string text, float dismissAfter = -1)
		{
			if (coroutine != null) StopCoroutine(coroutine);
			coroutine = StartCoroutine(ShowNotificationCoroutine(title, text, dismissAfter));
		}

		private IEnumerator ShowNotificationCoroutine(string title, string text, float dismissAfter)
		{
			if (IsNotificationShown)
			{
				Hide();
				yield return new WaitForSeconds(0.3f);
			}

			titleLabel.text = title;
			textLabel.text = text;

			Show();

			if (dismissAfter >= 0)
			{
				yield return new WaitForSeconds(dismissAfter);
				Hide();
			}
		}

		private void Show()
		{
			audioSource.clip = clip;
			audioSource.pitch = 1f;
			audioSource.PlayDelayed(0.3f);
			notification.RemoveFromClassList("notification--hidden");
		}

		private void Hide()
		{
			notification.AddToClassList("notification--hidden");
		}
	}
}