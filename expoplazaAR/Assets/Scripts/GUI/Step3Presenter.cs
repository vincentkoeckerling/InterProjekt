using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
	public class Step3Presenter : MonoBehaviour
	{
		private VisualElement step3;

		private TextField firstNameTextField;
		private TextField lastNameTextField;
		private DropdownField genderDropdownField;
		private TextField birthdayTextField;
		private TextField streetTextField;
		private TextField zipCodeTextField;
		private TextField cityTextField;

		private Button button;

		private void OnEnable()
		{
			step3 = GetComponent<UIDocument>().rootVisualElement.Q("step3");

			firstNameTextField = step3.Q<TextField>("firstName");
			lastNameTextField = step3.Q<TextField>("lastName");
			genderDropdownField = step3.Q<DropdownField>("gender");
			birthdayTextField = step3.Q<TextField>("birthday");
			streetTextField = step3.Q<TextField>("street");
			zipCodeTextField = step3.Q<TextField>("zipCode");
			cityTextField = step3.Q<TextField>("city");

			button = step3.Q<Button>("button");

			UpdateState();

			step3.RegisterCallback<InputEvent>((_) => UpdateState());
			genderDropdownField.RegisterValueChangedCallback((_) => UpdateState());
		}

		private void UpdateState()
		{
			button.SetEnabled(IsFilledOut());
		}

		private bool IsFilledOut()
		{
			var allFields = step3.Query<BaseField<string>>(className: "unity-base-field").ToList();
			return allFields.TrueForAll((field) => !string.IsNullOrEmpty(field.value));
		}
	}
}
