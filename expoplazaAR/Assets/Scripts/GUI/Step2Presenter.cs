using ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace GUI
{
	public class Step2Presenter : MonoBehaviour
	{
		[SerializeField] private UserInfo userInfo;
		
		private VisualElement step2;
	
		private TextField usernameTextField;
		private TextField emailTextField;
		private TextField passwordTextField;
		private TextField confirmPasswordTextField;

		private Button button;

		private void OnEnable()
		{
			step2 = GetComponent<UIDocument>().rootVisualElement.Q("step2");
		
			usernameTextField = step2.Q<TextField>("username");
			emailTextField = step2.Q<TextField>("email");
			passwordTextField = step2.Q<TextField>("password");
			confirmPasswordTextField = step2.Q<TextField>("confirmPassword");

			button = step2.Q<Button>("button");
			button.clicked += Submit;
			
			UpdateState();
		
			step2.RegisterCallback<InputEvent>((_) => UpdateState());
		}

		private void UpdateState()
		{
			button.SetEnabled(IsFilledOut());
		}

		private bool IsFilledOut()
		{
			var allFields = step2.Query<BaseField<string>>(className: "unity-base-field").ToList();
		
			return 
				allFields.TrueForAll((field) => !string.IsNullOrEmpty(field.value))
				&&
				passwordTextField.value.Equals(confirmPasswordTextField.value);
		}

		private void Submit()
		{
			userInfo.username = usernameTextField.value;
			userInfo.email = emailTextField.value;
			userInfo.password = passwordTextField.value;
		}
	}
}
