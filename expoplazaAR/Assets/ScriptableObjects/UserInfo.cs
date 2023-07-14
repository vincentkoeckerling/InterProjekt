using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "UserInfo", menuName = "ScriptableObjects/UserInfo", order = 0)]
    public class UserInfo : ScriptableObject
    {
        public string username;
        public string email;
        public string password;

        public string firstName;
        public string lastName;
        public string gender;
        public string birthday;
        public string street;
        public string zipCode;
        public string city;

        public List<string> additionalInfo;
    }
}