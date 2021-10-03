using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //int currentSceneIndex;

    private string masterBus = "Bus:/";
    public static bool isPaused = false;

    //public delegate void PauseGame();
    //public static event PauseGame OnPauseGame;

    private void Awake()
    {
        //Debug.Log(currentSceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            PauseGameplay();
            ResumeGame();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
           // ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
        FMODUnity.RuntimeManager.GetBus(masterBus).stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (SceneManager.sceneCountInBuildSettings == currentSceneIndex++)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex++);
        FMODUnity.RuntimeManager.GetBus(masterBus).stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        FMODUnity.RuntimeManager.GetBus(masterBus).stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PauseGameplay()
    {
        if (!isPaused)
        {
            //isPaused = true;

            Time.timeScale = 0;
            FMODUnity.RuntimeManager.GetBus(masterBus).setPaused(true);
            //OnPauseGame?.Invoke();
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            //isPaused = false;

            Time.timeScale = 1;
            FMODUnity.RuntimeManager.GetBus(masterBus).setPaused(false);
            //OnPauseGame?.Invoke();

        }
    }

    public void LoadCreditsScene()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 2);
    }

    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
