using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
    public class PopupPresenter : MonoBehaviour
    {
        [SerializeField] private UIDocument notificationUI;

        private VisualElement notification;
        private Label title;
        private Label text;

        private Coroutine coroutine;
        
        private bool IsNotificationShown => !notification.ClassListContains("notification-hidden");

        private void OnEnable()
        {
            notification = notificationUI.rootVisualElement.Q("notification");
            title = notification.Q<Label>("title");
            text = notification.Q<Label>("text");

            notification.Q<Button>("dismissButton").clicked += Hide;

            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            yield return new WaitForSeconds(3);
            ShowNotification("Tips", "Ich fürchte hier gibt es keine Fische mehr, du Hai!");
            yield return new WaitForSeconds(3);
            ShowNotification("Tips 2", "Es gibt wirklich keine Fische mehr Bastard");
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

            this.title.text = title;
            this.text.text = text;

            Show();
            
            if (dismissAfter >= 0)
            {
                yield return new WaitForSeconds(dismissAfter);
                Hide();
            }
        }

        private void Show()
        {
            notification.RemoveFromClassList("notification--hidden");
        }

        private void Hide()
        {
            notification.AddToClassList("notification--hidden");
        }
    }
}