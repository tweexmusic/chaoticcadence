using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 3;
    public ParticleSystem hitParticle;
    [HideInInspector]
    public bool isAlive = true;

    public Text healthText;

    public MenuLose menuLose;

    public delegate void PlayerDeath();

    public static event PlayerDeath OnPlayerDeath;

    private void Update()
    {
        healthText.text = playerHealth.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy Projectile" || collision.gameObject.tag == "Enemy")
        {
            if (EnemyManager.enemiesKillGoal != 0)
            {
                PlayerTakeDamage();
            }
        }
    }

    private void PlayerTakeDamage()
    {
        if (playerHealth > 1)
        {
            playerHealth--;
            ParticleSystem clone = Instantiate(hitParticle, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            OnPlayerDeath?.Invoke();
            playerHealth--;
            isAlive = false;

        }
    }

}
