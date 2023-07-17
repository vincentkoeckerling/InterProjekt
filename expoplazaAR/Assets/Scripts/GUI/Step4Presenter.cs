using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
    public class Step4Presenter : MonoBehaviour
    {
        [SerializeField] private UserInfo userInfo;

        private VisualElement step4;
        private List<Toggle> toggles;

        private void OnEnable()
        {
            step4 = GetComponent<UIDocument>().rootVisualElement.Q("step4");
            toggles = step4.Query<Toggle>(className: "toggle").ToList();

            var button = step4.Q<Button>("button");
            button.clicked += Submit;
        }

        private void Submit()
        {
            userInfo.additionalInfo =
                toggles
                    .FindAll(toggle => toggle.value)
                    .ConvertAll(toggle => toggle.text);
        }
    }
}