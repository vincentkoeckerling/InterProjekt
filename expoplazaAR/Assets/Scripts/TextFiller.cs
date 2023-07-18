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
        String formatedText = $"Username : {userInfo.username} \n" +
                              $"Email : {userInfo.email} \n" + 
                              $"Password : {userInfo.password}\n" +
                              $"Vorname : {userInfo.firstName}\n" +
                              $"Nachname : {userInfo.lastName}\n" +
                              $"Geschecht : {userInfo.gender}\n" +
                              $"Geburtsdatum {userInfo.birthday}\n" +
                              $"Adresse : {userInfo.street}\n" +
                              $"{userInfo.zipCode}\n"+
                              $"{userInfo.city}";
        infoText.text = formatedText;    
    }
}
