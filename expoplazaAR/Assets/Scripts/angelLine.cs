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

    private bool throwing;
    private Coroutine throwCoroutine;
    private bool lineRendered;

    private void Update()
    {
        if (Input.GetKeyDown(angleKey))
        {
            if (!throwing)
            {
                throwCoroutine = StartCoroutine(StartThrowWithDelay(angelDelayTime));
            }
            else
            {
                // Repress the mouse click, interrupt the coroutine, and start a new throw
                StopCoroutine(throwCoroutine);
                throwing = false;
                lineRendered = false;
                lr.enabled = false;
            }
        }

        if (angelCdTimer > 0)
        {
            angelCdTimer -= Time.deltaTime;
        }
    }

    private IEnumerator StartThrowWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartThrow();
    }

    private void StartThrow()
    {
        if (angelCdTimer > 0) return;

        throwing = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxLineDistance, whatIsLandable))
        {
            angelPoint = hit.point;
        }
        else
        {
            angelPoint = cam.position + cam.forward * maxLineDistance;
        }

        lr.enabled = true;
        lr.SetPosition(0, angelTip.position);
        lr.SetPosition(1, angelPoint);
        lineRendered = true;
    }

    private void LateUpdate()
    {
        if (lineRendered)
        {
            lr.SetPosition(0, angelTip.position);
        }
    }

    private void StopThrow()
    {
        throwing = false;

        angelCdTimer = angelCD;
        lr.enabled = false;
        lineRendered = false;
    }
}