using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerSwitchScene : MonoBehaviour
{
    public String sceneName;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // Switch Scene
        SceneManager.LoadScene(sceneName);
    }
}
