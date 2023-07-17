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
        Vector3 randomPointOnMesh = mesh1.vertices[UnityEngine.Random.Range(0, mesh1.vertices.Length)];
        
        pos = new Vector3(
            randomPointOnMesh.x * meshRenderer.transform.localScale.x + meshRenderer.transform.position.x,
            randomPointOnMesh.y * meshRenderer.transform.localScale.y + meshRenderer.transform.position.y,
            randomPointOnMesh.z * meshRenderer.transform.localScale.z + meshRenderer.transform.position.z);
        
        return pos;
    }

    private void FixedUpdate()
    {
        moveAround.transform.position = GetRandomPositionOnMesh(_mesh);
    }
}
