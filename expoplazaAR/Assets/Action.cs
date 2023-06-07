using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

    public GameObject hideThis;
    public GameObject showThis;

    private void OnTriggerEnter(Collider other)
    {
        hideThis.SetActive(false);
        showThis.SetActive(true);
    }
}
