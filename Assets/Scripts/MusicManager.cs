using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    FMODUnity.StudioEventEmitter musicEvent;
    FMOD.Studio.EventDescription eventDescription;

    public static int totalEventLength;
    public static int timeRemaining;

    public Text timeRemainingText;

    public delegate void TimeExpired();

    public static event TimeExpired OnTimeExpire;

    private bool musicStarted = false;

    private void Awake()
    {
        timeRemaining = 0;
        musicEvent = GetComponent<FMODUnity.StudioEventEmitter>();
        StartGameCountdown.OnGameStart += OnGameStart;
        
    }

    private void OnGameStart()
    {
        musicEvent.Play();
        musicEvent.EventInstance.getDescription(out eventDescription);

        musicStarted = true;

        int length;
        eventDescription.getLength(out length);
        totalEventLength = length / 1000;
        timeRemaining = totalEventLength;
        Debug.Log(totalEventLength);
    }



    private void Update()
    {
        if(timeRemaining != 0)
        {
            UpdateTimer();
        }

        if (timeRemaining == 0 && musicStarted)

        {
            OnTimeExpire?.Invoke();
        }        
    }

    private void UpdateTimer()
    {
        int position;
        musicEvent.EventInstance.getTimelinePosition(out position);
        timeRemaining = (totalEventLength - (position / 1000));
        timeRemainingText.text = timeRemaining.ToString();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        StartGameCountdown.OnGameStart -= OnGameStart;
    }
}
