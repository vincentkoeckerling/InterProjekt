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
    private Coroutine _throwCoroutine;
    private bool lineRendered;
    private bool reelDelayStarted;

    private GameObject _springer;
    private Transform _springerParent;
    private Vector3 _springerPosition;
    private FishingGame _fishingGame;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if(_animator == null)
            Debug.LogError("Animator not found");

        _springer = GameObject.Find("springer");
        if(_springer == null)
            Debug.LogError("springer not found");
        
        _springerParent = _springer.transform.parent;
        _springerPosition = _springer.transform.localPosition;

        _fishingGame = GameObject.Find("FishingGame").GetComponent<FishingGame>();
        if(_fishingGame == null)
            Debug.LogError("FishingGame not found");
    }

    private void Update()
    {
        if (Input.GetKeyDown(angleKey))
        {
            if (!throwing && !lineRendered)
            {
                _animator.SetTrigger("wurf");
                _throwCoroutine = StartCoroutine(StartThrowWithDelay(angelDelayTime));
            }
        }

        if (Input.GetKeyDown(reelKey) && lineRendered)
        {
            if (!reelDelayStarted)
            {
                _animator.SetTrigger("catch");

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
        _fishingGame.fishingRodIsInWater = true;
    }
    
    private void AttachSpringerToFishingRod()
    {
        _springer.transform.parent = _springerParent.transform;
        _springer.transform.localPosition = _springerPosition;
        _fishingGame.fishingRodIsInWater = false;
        
        _fishingGame.fishingRodIsCatching = true;
        // set _fishingGame.fishingRodIsCatching to false after 0.1 second
        Invoke(nameof(StopCatching), 0.1f);
        Invoke(nameof(AttachFishToSpringer), 0.05f);
    }

    private void AttachFishToSpringer()
    {
        if (!_fishingGame.fishIsCatched) return;
        
        _fishingGame.fishIsCatched = false;
        _fishingGame.fish.SetActive(true);
        
        Invoke(nameof(FadeOutFish), 5f);
    }
    
    private void FadeOutFish()
    {
        _fishingGame.fish.SetActive(false);
    }

    private void StopCatching()
    {
        // you might call it a workaround, i call it a perfectly working solution
        _fishingGame.fishingRodIsCatching = false;
    }

    private void StartThrow()
    {
        if (angelCdTimer > 0) return;

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, maxLineDistance, whatIsLandable))
        {
            angelPoint = hit.point;
        }
        else
        {
            angelPoint = cam.position + cam.forward * maxLineDistance;
        }
        
        PlaceSpringerIntoWorld(angelPoint);

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
