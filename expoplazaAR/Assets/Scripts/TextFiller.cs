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
                              $"nachname : {userInfo.lastName}\n" +
                              $"Geschecht : {userInfo.gender}\n" +
                              $"Geburtsdatum {userInfo.birthday}\n" +
                              $"Straße:  {userInfo.street}\n" +
                              $"Zip Code : {userInfo.zipCode}\n"+
                              $"Stadt: {userInfo.city}";
        infoText.text = formatedText;    
    }
}
