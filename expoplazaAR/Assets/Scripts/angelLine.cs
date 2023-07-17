using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angelLine : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform angelTip;
    public LayerMask whatIsLandable;
    public LineRenderer lr;

    [Header("angelLine")]
    public float maxLineDistance;
    public float angelDelayTime;

    private Vector3 angelPoint;

    [Header("Cooldown")]
    public float angelCD;
    private float angelCdTimer;

    [Header("Input")]
    public KeyCode angleKey = KeyCode.Mouse0;
    public KeyCode reelKey = KeyCode.Mouse1;
    public float reelDelay;

    private bool throwing;
    private Coroutine throwCoroutine;
    private bool lineRendered;
    private bool reelDelayStarted;

    private GameObject _springer;
    private Transform _springerParent;
    private Vector3 _springerPosition;

    private void Start()
    {
        _springer = GameObject.Find("springer");
        if(_springer == null)
            Debug.LogError("springer not found");
        _springerParent = _springer.transform.parent;
        _springerPosition = new Vector3(_springer.transform.localPosition.x, _springer.transform.localPosition.y, _springer.transform.localPosition.z);
        Debug.Log("springer position: " + _springerPosition.x + " " + _springerPosition.y + " " + _springerPosition.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(angleKey))
        {
            if (!throwing && !lineRendered)
            {
                throwCoroutine = StartCoroutine(StartThrowWithDelay(angelDelayTime));
            }
        }

        if (Input.GetKeyDown(reelKey) && lineRendered)
        {
            if (!reelDelayStarted)
            {
                reelDelayStarted = true;
                Invoke(nameof(UnrenderLine), reelDelay);
            }
        }

        if (angelCdTimer > 0)
        {
            angelCdTimer -= Time.deltaTime;
        }
    }

    private IEnumerator StartThrowWithDelay(float delay)
    {
        throwing = true;
        yield return new WaitForSeconds(delay);
        StartThrow();
    }

    private void PlaceSpringerIntoWorld(Vector3 position)
    {
        _springer.transform.parent = null;
        _springer.transform.position = position;
    }
    
    private void AttachSpringerToFishingRod()
    {
        _springer.transform.parent = _springerParent.transform;
        _springer.transform.localPosition = _springerPosition;
    }

    private void StartThrow()
    {
        if (angelCdTimer > 0) return;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxLineDistance, whatIsLandable))
        {
            angelPoint = hit.point;
            PlaceSpringerIntoWorld(angelPoint);
        }
        else
        {
            angelPoint = cam.position + cam.forward * maxLineDistance;
        }

        lr.enabled = true;
        lr.SetPosition(0, angelTip.position);
        lr.SetPosition(1, angelPoint);
        lineRendered = true;
        DisableLeftMouseClick();
    }

    private void LateUpdate()
    {
        if (lineRendered)
        {
            lr.SetPosition(0, angelTip.position);
        }
    }

    private void UnrenderLine()
    {
        lineRendered = false;
        lr.enabled = false;
        reelDelayStarted = false;
        EnableLeftMouseClick();
        throwing = false; // Reset the throwing flag
        
        AttachSpringerToFishingRod();
    }

    private void DisableLeftMouseClick()
    {
        angleKey = KeyCode.None;
    }

    private void EnableLeftMouseClick()
    {
        angleKey = KeyCode.Mouse0;
    }
}
