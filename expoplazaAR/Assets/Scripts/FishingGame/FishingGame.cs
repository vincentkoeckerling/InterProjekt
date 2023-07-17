using System;
using System.Collections;
using System.Collections.Generic;
using GUI;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingGame : MonoBehaviour
{
    private Water waterParticleSystem;
    private bool _fishSpawned;
    private AudioSource _audioSource;
    private int _fishCounter;
    private NotificationPresenter _notificationPresenter;
    private bool _playerShouldMove;
    private Vector3 _playerFishingPosition;

    public int fishIsCatchableForXSeconds = 5;
    public Vector2Int fishSpawnTimeRange = new(5, 30);
    public Vector3 fishPosition;
    public GameObject player;

    private void Start()
    {
        waterParticleSystem = GetComponentInChildren<Water>();

        if(waterParticleSystem == null)
            Debug.LogError("No water particle system found");
        
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
            Debug.LogError("No audio source found");
        
        _notificationPresenter = GetComponentInChildren<NotificationPresenter>();
        if(_notificationPresenter == null)
            Debug.LogError("No notification presenter found");
        
        StartCoroutine(SpawnFish());
    }


    private void Update()
    {
        if (!_fishSpawned) return;
        
        // check if space bar is pressed
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        _fishSpawned = false;
        _fishCounter++;
        
        // stop particle system
        waterParticleSystem.Stop();
                
        // play sound
        _audioSource.Play();
        
        Debug.Log("You caught a fish!");

        if (_fishCounter == 3)
        {
            _playerFishingPosition = player.transform.position;
        }
        
        if(_fishCounter >= 3)
        {
            _notificationPresenter.ShowNotification("Du hast hier genug Fische gefangen!",
                "Vielleicht solltest du dir einen neuen Angelplatz suchen.");
            _playerShouldMove = true;
        }
    }

    private IEnumerator SpawnFish()
    {
        int randomTime = UnityEngine.Random.Range(fishSpawnTimeRange.x, fishSpawnTimeRange.y);
        Debug.Log("Waiting for " + randomTime + " seconds");
        yield return new WaitForSeconds(randomTime);

        
        // move particle to position
        waterParticleSystem.transform.position = fishPosition;
        
        
        // play particle system
        waterParticleSystem.Play();
        
        
        _fishSpawned = true;
        Debug.Log("Fish spawned");
        
        // wait 5 seconds
        yield return new WaitForSeconds(fishIsCatchableForXSeconds);
        
        _fishSpawned = false;
        StartCoroutine(SpawnFish());
    }
}
