using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
    [RequireComponent(typeof(UIDocument))]
    public class ModalPresenter : MonoBehaviour
    {
        private VisualElement modal;
        private Label titleLabel;
        private Label textLabel;

        private Coroutine coroutine;

        private void OnEnable()
        {
            modal = GetComponent<UIDocument>().rootVisualElement.Q("modal");
            titleLabel = modal.Q<Label>("title");
            textLabel = modal.Q<Label>("text");

            modal.Query<Button>(className: "button").ForEach(btn => btn.clicked += Hide);
        }

        public void ShowModal(string title, string text)
        {
            titleLabel.text = title;
            textLabel.text = text;

            Show();
        }

        private void Show()
        {
            modal.RemoveFromClassList("modal--hidden");
        }

        private void Hide()
        {
            modal.AddToClassList("modal--hidden");
        }
    }
}