using System;
using UnityEngine;
using System.Collections;

namespace GUI
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private NotificationPresenter notificationPresenter;
        [SerializeField] private ModalPresenter modalPresenter;
        
        private void OnEnable()
        {
            // StartCoroutine(TestNotification());
            StartCoroutine(TestModal());
        }

        private IEnumerator TestNotification()
        {
            yield return new WaitForSeconds(3);
            notificationPresenter.ShowNotification("Tips", "Ich fürchte hier gibt es keine Fische mehr, du Hai!");
            yield return new WaitForSeconds(5);
            notificationPresenter.ShowNotification("Tips 2", "Es gibt wirklich keine Fische mehr Bastard", 10);
        }

        private IEnumerator TestModal()
        {
            yield return new WaitForSeconds(3);
            modalPresenter.ShowModal("Kurze Frage", "Spielst du gerne Videospiele?");
        }
    }
}