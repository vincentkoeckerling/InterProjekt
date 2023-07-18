using System;
using System.Collections;
using System.Collections.Generic;
using GUI;
using Helper;
using UnityEngine;
using UnityEngine.Serialization;

public class FishingGame : MonoBehaviour
{
    private Water waterParticleSystem;
    private bool _fishSpawned;
    private AudioSource _audioSource;
    private int _fishCounter;
    private NotificationPresenter _notificationPresenter;
    private ModalPresenter _modalPresenter;
    private bool _playerShouldMove;
    private Vector3 _playerFishingPosition;
    private GameObject _glitchCube;
    private ImageGlitch _imageGlitch;
    private bool _endGameActivated;

    public int fishIsCatchableForXSeconds = 5;
    public Vector2Int fishSpawnTimeRange = new(5, 30);
    public GameObject fishPosition;
    public GameObject player;
    public float playerNeedsToMoveAwayFromFishingSpot = 4f;
    public bool fishingRodIsInWater;
    public bool fishingRodIsCatching;
    public GameObject fish;
    public bool fishIsCatched;
    public GameObject polaroid;

    private void Start()
    {
        if(polaroid == null)
            Debug.LogError("No polaroid found");
        polaroid.SetActive(false);
        
        if(fish == null)
            Debug.LogError("No fish found");
        fish.SetActive(false);
        
        _imageGlitch = fish.GetComponentInChildren<ImageGlitch>();
        if(_imageGlitch == null)
            Debug.LogError("No image glitch found on fish");
        _imageGlitch.enabled = false;
        
        waterParticleSystem = GetComponentInChildren<Water>();
        if(waterParticleSystem == null)
            Debug.LogError("No water particle system found");
        
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
            Debug.LogError("No audio source found");
        
        _notificationPresenter = GetComponentInChildren<NotificationPresenter>();
        if(_notificationPresenter == null)
            Debug.LogError("No notification presenter found");
        
        _modalPresenter = GetComponentInChildren<ModalPresenter>();
        if(_modalPresenter == null)
            Debug.LogError("No modal presenter found");
        
        if(player == null)
            Debug.LogError("No player found");
        
        _glitchCube = GameObject.Find("GlitchCube");
        if(_glitchCube == null)
            Debug.LogError("No glitch cube found");
        _glitchCube.SetActive(false);
        
        StartCoroutine(SpawnFish());
    }


    private void Update()
    {
        if (!_endGameActivated && _playerShouldMove)
        {
            // check if player has moved from his fishing spot
            if (Vector3.Distance(_playerFishingPosition, player.transform.position) > playerNeedsToMoveAwayFromFishingSpot)
            {
                _notificationPresenter.ShowNotification("Gut gemacht!",
                    "Du hast dich weit genug vom Angelplatz entfernt. Vielleicht kannst du jetzt erneut Fische fangen.");
                _playerShouldMove = false;
                _fishCounter = 0;
                
                StartCoroutine(CameraHelper.TakePhoto());
            }
        }
        
        
        if (!_fishSpawned) return;
        
        
        if (!fishingRodIsCatching) return;
        _fishSpawned = false;
        _fishCounter++;
        
        // stop particle system
        waterParticleSystem.Stop();
                
        // play sound
        _audioSource.Play();
        
        Debug.Log("You caught a fish!");
        fishIsCatched = true;

        switch (_fishCounter)
        {
            case 3:
                _playerFishingPosition = player.transform.position;
                StartCoroutine(CameraHelper.TakePhoto());
                _modalPresenter.ShowModal("Wichtige Frage", "Möchtest du ein Foto von deinen Fischen machen?", null);
                break;
            case 4:
                _notificationPresenter.ShowNotification("Du hast hier genug Fische gefangen!",
                    "Vielleicht solltest du dir einen neuen Angelplatz suchen.");
                _playerShouldMove = true;
                break;
            case 5:
                _notificationPresenter.ShowNotification("Hey!",
                    "Vielleicht solltest du dir tatsächlich JETZT einen neuen Angelplatz suchen.");
                _playerShouldMove = true;
                break;
            case 6:
                _notificationPresenter.ShowNotification("HALLO!",
                    "HIER GIBT ES NICHTS MEHR, BITTE GEH WOANDERS HIN.");
                _playerShouldMove = true;
                _imageGlitch.enabled = true;
                break;
        }

        if (_fishCounter != 7) return;
        fish.SetActive(false);
        polaroid.SetActive(true);
        _glitchCube.SetActive(true);

        _endGameActivated = true;
        
        // call hidePolaroid after 5 seconds
        Invoke(nameof(HidePolaroid), 5f);
    }

    private void HidePolaroid()
    {
        polaroid.SetActive(false);
    }

    private IEnumerator SpawnFish()
    {
        if (!fishingRodIsInWater)
        {
            Debug.Log("Fishing rod is not in water");
            
            // wait 5 seconds
            yield return new WaitForSeconds(fishIsCatchableForXSeconds);
            StartCoroutine(SpawnFish());

            yield break;
        };
        
        int randomTime = UnityEngine.Random.Range(fishSpawnTimeRange.x, fishSpawnTimeRange.y);
        Debug.Log("Waiting for " + randomTime + " seconds");
        yield return new WaitForSeconds(randomTime);

        
        // move particle to position
        waterParticleSystem.transform.position = fishPosition.transform.position;
        
        
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
