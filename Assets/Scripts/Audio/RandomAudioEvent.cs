using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioEvents;


public class RandomAudioEvent : MonoBehaviour
{
    [SerializeField] private AudioEvent audioEvent;
    [SerializeField] private AudioSource audioSource;
    public RangedFloat delay;
    private float nextEventTime;
    private bool isActive;

    void Start()
    {
        UpgradeArea.OnSafeAreaEntered += DisableEvents;
        UpgradeArea.OnSafeAreaDisabled += EnableEvents;
        isActive = true;
    }
    void OnDestroy()
    {
        UpgradeArea.OnSafeAreaEntered -= DisableEvents;
        UpgradeArea.OnSafeAreaDisabled -= EnableEvents;
    }
    void Update()
    {
        if(!isActive)
            return;
        if(Time.time >= nextEventTime)
        {
            nextEventTime = Time.time + delay.RandomValue;
            audioEvent.Play(audioSource);
        }
    }

    private void EnableEvents()
    {
        isActive = true;
    }
    private void DisableEvents()
    {
        isActive = false;
    }


}
