using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public EnemyProjectile enemyProjectile;

    [SerializeField]
    float horizontalMovementSpeed;
    [SerializeField]
    float verticalMovementSpeed;

    FMODUnity.StudioEventEmitter enemySpawnAudio;

    public ParticleSystem enemyDeathParticle;

    [HideInInspector]
    public Animator shakeAnticipation;


    private bool isEnemyAlive = true;
    private bool enemyShake;

    SpriteRenderer enemySpriteRender;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySpawnAudio = GetComponent<FMODUnity.StudioEventEmitter>();
        shakeAnticipation = GetComponent<Animator>();
        enemySpriteRender = GetComponent<SpriteRenderer>();
    }

    

    private void Update()
    {
        DestroyEnemy();
        ChangeColor();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Projectile" || collision.gameObject.tag == "Player")
        {
            isEnemyAlive = false;
            DestroyEnemy();
        }
    }

    private void ChangeColor()
    {
        if (!EnemyManager.goalReached)
        {
            enemySpriteRender.color = ColorSetter.instance.GetBaseColor;
        }
        else
        {
            enemySpriteRender.color = ColorSetter.instance.GetGodModeColor(false);
        }
    }

    public void ShakeEnemyAnimation()
    {
        enemyShake = !enemyShake;
        shakeAnticipation.SetBool("enemyShake", enemyShake);
    }

    private void DestroyEnemy()
    {
        if (!isEnemyAlive)
        {
            shakeAnticipation.SetBool("enemyShake", false);
            ParticleSystem clone = Instantiate(enemyDeathParticle, gameObject.transform.position, Quaternion.identity);
            EnemyManager.enemiesKilled++;
            Destroy(gameObject);
        }
    }

    public void EnemyShoot()
    {
        EnemyProjectile clone = Instantiate(enemyProjectile, gameObject.transform.position, Quaternion.identity);
    }

    public void PlayEnemySpawnAudio()
    {
        if (!enemySpawnAudio.IsPlaying())
        {
            enemySpawnAudio.Play();
        }
    }
}