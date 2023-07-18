using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using ScriptableObjects;

public class TextFiller : MonoBehaviour
{

	public TMP_Text infoText;

	public UserInfo userInfo;
	// Start is called before the first frame update
	void Start()
	{
		infoText = GetComponent<TMP_Text>();
	}

	// Update is called once per frame
	void Update()
	{
		String additionalInfo = "";
		foreach (var str in userInfo.additionalInfo)
		{
			additionalInfo += $"  - {str}\n";
		}

		String formatedText = $"Username :\t{userInfo.username} \n" +
									 $"Email :\t\t{userInfo.email} \n" +
									 $"Password :\t{userInfo.password}\n" +
									 $"Vorname :\t{userInfo.firstName}\n" +
									 $"Nachname :\t{userInfo.lastName}\n" +
									 $"Geschlecht :\t{userInfo.gender}\n" +
									 $"Geburtsdatum :\t{userInfo.birthday}\n" +
									 $"Adresse :\t{userInfo.street}\n" +
									 $"\t\t{userInfo.zipCode} {userInfo.city}\n" +
									 $"Weitere Informationen :\n" +
									 additionalInfo;
		infoText.text = formatedText;
	}
}
