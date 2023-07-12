using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
	[SerializeField]
	private UIDocument step1;

	[SerializeField]
	private UIDocument step2;

	[SerializeField]
	private UIDocument step3;

	[SerializeField]
	private string destinationSceneName;

	private void Start()
	{
		step1.enabled = true;
		step2.enabled = false;
		step3.enabled = false;
	}

    private void OnEnable() {
        var step1Button = step1.rootVisualElement.Q<Button>("button1");
		var step2Button = step2.rootVisualElement.Q<Button>("button2");
		var step3Button = step3.rootVisualElement.Q<Button>("button3");

        step1Button.clicked += onStep1Click;
        step2Button.clicked += onStep2Click;
        step3Button.clicked += onStep3Click;
    }

	private void onStep1Click()
	{
		step1.enabled = false;
		step2.enabled = true;
	}

	private void onStep2Click()
	{
		step2.enabled = false;
		step3.enabled = true;
	}

	private void onStep3Click()
	{
		SceneManager.LoadScene(destinationSceneName);
	}
}
