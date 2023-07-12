using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Step2Presenter : MonoBehaviour
{
	private VisualElement step2;

	private TextField firstNameTextField;
	private TextField lastNameTextField;
	private DropdownField genderDropdownField;
	private TextField emailTextField;
	private TextField streetTextField;
	private TextField zipCodeTextField;
	private TextField cityTextField;

	private Button button;

	private void OnEnable()
	{
		step2 = GetComponent<UIDocument>().rootVisualElement.Q("step2");

		firstNameTextField = step2.Q<TextField>("firstName");
		lastNameTextField = step2.Q<TextField>("lastName");
		genderDropdownField = step2.Q<DropdownField>("gender");
		emailTextField = step2.Q<TextField>("email");
		streetTextField = step2.Q<TextField>("street");
		zipCodeTextField = step2.Q<TextField>("zipCode");
		cityTextField = step2.Q<TextField>("city");

		button = step2.Q<Button>("button");

		UpdateState();

		step2.RegisterCallback<InputEvent>((_) => UpdateState());
		genderDropdownField.RegisterValueChangedCallback((_) => UpdateState());
	}

	private void UpdateState()
	{
		button.SetEnabled(IsFilledOut());
	}

	private bool IsFilledOut()
	{
		var allFields = step2.Query<BaseField<string>>(className: "unity-base-field").ToList();
		return allFields.TrueForAll((field) => field.value != null && field.value != "");
	}
}
