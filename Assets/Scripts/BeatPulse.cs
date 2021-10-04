using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPulse : MonoBehaviour
{
    [Range(0, 1)]
    public float minAlpha;
    [Range(0,1)]
    public float maxAlpha;
    float fadeSpeed;

    SpriteRenderer sprite;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        MidiManagerBeat.OnBeatChange += OnBeatChange;
    }

    private void OnBeatChange(int currentNoteValue, string currentNoteName)
    {
        StartCoroutine(BeatPulseTimer(minAlpha, fadeSpeed));
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeSpeed = 60 / MidiManagerBeat.instance.musicTempo;
    }

    

    IEnumerator BeatPulseTimer(float endAlphaValue, float timeLength)
    {
        sprite.material.color = new Color (1f, 1f, 1f, maxAlpha);
        float alpha = sprite.material.color.a;

        for (float currentAlphaValue = 0.0f; currentAlphaValue < 1f; currentAlphaValue += Time.deltaTime / timeLength)
        {
            Color newAlpha = new Color(1, 1, 1, Mathf.Lerp(alpha, endAlphaValue, currentAlphaValue));
            sprite.material.color = newAlpha;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        MidiManagerBeat.OnBeatChange -= OnBeatChange;
    }
}
