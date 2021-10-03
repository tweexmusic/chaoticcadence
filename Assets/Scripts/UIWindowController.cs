using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowController : MonoBehaviour
{
    public MenuLose menuLose;
    public MenuLose levelCompleteMenu;
    public MenuLose pauseMenu;

    private bool menuActive;

    private void Awake()
    {
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;
        MusicManager.OnTimeExpire += OnTimeExpire;
        //SceneLoader.OnPauseGame += OnPauseGame;
    }


    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
        MusicManager.OnTimeExpire -= OnTimeExpire;
        //SceneLoader.OnPauseGame -= OnPauseGame;


        StopAllCoroutines();

    }

    IEnumerator ShowMenu(MenuLose menu, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(menu);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(ShowMenu(menuLose, 2f));
    }

    private void OnTimeExpire()
    {
        if (!menuActive)
        {
            menuActive = true;

            if (EnemyManager.enemiesKillGoal > 0)
            {
                StartCoroutine(ShowMenu(menuLose, 2f));
            }
            else
            {
                StartCoroutine(ShowMenu(levelCompleteMenu, 2f));
            }
        }
    }

    /*
    private void OnPauseGame()
    {
        var pauseMenuObject = FindObjectOfType<MenuLose>();
        {
            if (pauseMenuObject == null)
            {
                menuActive = true;
                StartCoroutine(ShowMenu(pauseMenu, 0f));
            }
            else if (pauseMenuObject != null && !menuActive)
            {
                pauseMenu.canvasGroup.alpha = 1;
            }

            else if (pauseMenuObject != null && !SceneLoader.isPaused)
            {
                Debug.Log("Unpause");
                pauseMenu.canvasGroup.alpha = 0;

            }
        }
    
    }
    */
        

}
