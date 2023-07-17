using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace GUI
{
	public class Test : MonoBehaviour
	{
		[SerializeField] private NotificationPresenter notificationPresenter;
		[SerializeField] private ModalPresenter modalPresenter;

		public void TestNotification()
		{
			notificationPresenter.ShowNotification("Tips", "Ich fürchte hier gibt es keine Fische mehr, du Hai!");
		}

		public void TestModal()
		{
			modalPresenter.ShowModal("Kurze Frage", "Spielst du gerne Videospiele?", (b) => Debug.Log(b));
		}
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(Test))]
	public class TestEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			Test test = (Test)target;
			if (GUILayout.Button("Test Notification"))
			{
				test.TestNotification();
			}

			if (GUILayout.Button("Test Modal"))
			{
				test.TestModal();
			}
		}
	}
#endif
}