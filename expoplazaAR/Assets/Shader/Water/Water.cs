using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEditor;
using UnityEngine;

public class Water : MonoBehaviour
{
    ParticleSystem[] particleSystems;
    
    // Start is called before the first frame update
    void Start()
    {
        // get all children particle systems
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void Play()
    {
        foreach (ParticleSystem particleSystemObj in particleSystems)
        {
            particleSystemObj.Play();
        }
    }
    
    public void Stop()
    {
        foreach (ParticleSystem particleSystemObj in particleSystems)
        {
            particleSystemObj.Stop();
        }
    }
}

[CustomEditor(typeof(Water))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Water test = (Water)target;
        if (GUILayout.Button("Play Water Animation"))
        {
            test.Play();
        }
    }
}