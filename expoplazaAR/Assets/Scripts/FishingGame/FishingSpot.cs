using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public GameObject moveAround;

    private Mesh _mesh;
    
    void Start()
    {
        _mesh = meshRenderer.GetComponent<MeshFilter>().mesh;
    }


    private Vector3 GetRandomPositionOnMesh(Mesh mesh1)
    {
        Vector3 pos = Vector3.zero;
        var randomPointOnMesh = mesh1.vertices[UnityEngine.Random.Range(0, mesh1.vertices.Length)] * 2000;
        pos = randomPointOnMesh;
        
        return pos;
    }

    private void FixedUpdate()
    {
        moveAround.transform.position = GetRandomPositionOnMesh(_mesh);
    }
}
