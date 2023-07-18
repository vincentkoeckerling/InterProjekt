using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerSwitchScene : MonoBehaviour
{
    public String sceneName;
    public GameObject player;

    private void Update()
    {
        // if distance between transform and player is less than 1.5f
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            // Switch Scene
            SceneManager.LoadScene(sceneName);
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // Switch Scene
        SceneManager.LoadScene(sceneName);
    }
}
