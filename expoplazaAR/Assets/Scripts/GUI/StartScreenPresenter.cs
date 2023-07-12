using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartScreenPresenter : MonoBehaviour
{

	[SerializeField]
	private string destinationSceneName;

	private VisualElement step1;
	private VisualElement step2;
	private VisualElement step3;

	private void OnEnable()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		step1 = root.Q("step1");
		step2 = root.Q("step2");
		step3 = root.Q("step3");

		step1.Q<Button>("button").clicked += onStep1Click;
		step2.Q<Button>("button").clicked += onStep2Click;

		var button3 = step3.Q<Button>("button");
		button3.clicked += onStep3Click;
		button3.SetEnabled(false);

		step3.Q<Toggle>("toggle").RegisterValueChangedCallback(e => button3.SetEnabled(e.newValue));
	}

	private void onStep1Click()
	{
		step1.style.display = DisplayStyle.None;
		step2.style.display = DisplayStyle.Flex;
	}

	private void onStep2Click()
	{
		step2.style.display = DisplayStyle.None;
		step3.style.display = DisplayStyle.Flex;
	}

	private void onStep3Click()
	{
		SceneManager.LoadScene(destinationSceneName);
	}
}
