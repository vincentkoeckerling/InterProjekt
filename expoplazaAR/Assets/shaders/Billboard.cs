using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_cam.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 90f, 90f);
    }
}
