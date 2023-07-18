using Helper;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace GUI
{
	public class StartScreenPresenter : MonoBehaviour
	{

		[SerializeField]
		private string destinationSceneName;

		private VisualElement step1;
		private VisualElement step2;
		private VisualElement step3;
		private VisualElement step4;
		private VisualElement step5;

		private void OnEnable()
		{
			var root = GetComponent<UIDocument>().rootVisualElement;

			step1 = root.Q("step1");
			step2 = root.Q("step2");
			step3 = root.Q("step3");
			step4 = root.Q("step4");
			step5 = root.Q("step5");

			step1.Q<Button>("button").clicked += OnStep1Click;
			step2.Q<Button>("button").clicked += OnStep2Click;
			step3.Q<Button>("button").clicked += OnStep3Click;
			step4.Q<Button>("button").clicked += OnStep4Click;

			var button5 = step5.Q<Button>("button");
			button5.clicked += OnStep5Click;
			button5.SetEnabled(false);

			step5.Q<Toggle>("toggle").RegisterValueChangedCallback(e => button5.SetEnabled(e.newValue));
		}

		private void OnStep1Click()
		{
			step1.style.display = DisplayStyle.None;
			step2.style.display = DisplayStyle.Flex;
		}

		private void OnStep2Click()
		{
			step2.style.display = DisplayStyle.None;
			step3.style.display = DisplayStyle.Flex;
		}

		private void OnStep3Click()
		{
			step3.style.display = DisplayStyle.None;
			step4.style.display = DisplayStyle.Flex;
		}

		private void OnStep4Click()
		{
			step4.style.display = DisplayStyle.None;
			step5.style.display = DisplayStyle.Flex;
		}

		private void OnStep5Click()
		{
			SceneManager.LoadScene(destinationSceneName);
		}
	}
}
