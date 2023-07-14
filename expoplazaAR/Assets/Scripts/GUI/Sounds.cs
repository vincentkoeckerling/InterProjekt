using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace GUI
{
    public class Sounds : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;

        [SerializeField] private string className;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            var ui = GetComponent<UIDocument>().rootVisualElement;

            ui.Query(className: className).ForEach(element => element.RegisterCallback<ClickEvent>(PlaySound));
        }

        private void PlaySound(EventBase _)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.Play();
        }
    }
}