using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameCountdown : MonoBehaviour
{
    private int countDownCounter = 3;

    public Text countDownText;
    private bool coroutineActive = false;

    [Range(0, 1)]
    public float maxAlpha = 1;
    float fadeSpeed;

    // Delegate for StartGame event.
    public delegate void StartGame();

    /// <summary>
    /// Event used to tell other classes when game starts.
    /// Takes int and string parameters from delegate.
    /// </summary>
    public static event StartGame OnGameStart;


    private void Awake()
    {
        //countDownText = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        countDownText.text = countDownCounter.ToString();
        countDownText.CrossFadeAlpha(0f, 1f, false);
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Countdown()
    {
        if (countDownCounter > 0 && !coroutineActive)
        {
            coroutineActive = true;
            StartCoroutine(StartGameCountdownTimer());
        }

        if (countDownCounter == 0 && !coroutineActive)
        {
            coroutineActive = true;
            StartCoroutine(StartGameCoroutine());
        }
    }

    
    IEnumerator StartGameCountdownTimer()
    {
        countDownText.CrossFadeAlpha(1f, 0f, false);
        countDownText.text = countDownCounter.ToString();
        countDownText.CrossFadeAlpha(0f, 1f, false);

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/game_counter");

        yield return new WaitForSeconds(1f);
        countDownCounter--;
        coroutineActive = false;
    }

    IEnumerator StartGameCoroutine()
    {
        countDownText.CrossFadeAlpha(1f, 0f, false);
        countDownText.text = "GO";
        countDownText.CrossFadeAlpha(0f, 2f, false);

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/game_counter");

        yield return new WaitForSeconds(2f);
        OnGameStart?.Invoke();
        //coroutineActive = false;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Start Counter", 1);
        //Debug.Log("Start!!");
    }

    /*
    IEnumerator CountDownPulse(float endAlphaValue, float timeLength)
    {
        countDownText.material.color = new Color(1f, 1f, 1f, maxAlpha);
        countDownText.text = countDownCounter.ToString();

        float alpha = countDownText.material.color.a;

        for (float currentAlphaValue = 0.0f; currentAlphaValue < 1f; currentAlphaValue += Time.deltaTime / timeLength)
        {
            Color newAlpha = new Color(1, 1, 1, Mathf.Lerp(alpha, endAlphaValue, currentAlphaValue));
            countDownText.material.color = newAlpha;
            coroutineActive = false;
        }

        countDownCounter--;

        yield return null;

    }
    */
}
