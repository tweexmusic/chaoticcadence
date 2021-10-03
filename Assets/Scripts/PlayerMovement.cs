using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public PlayerProjectile playerProjectile;
    public PlayerHealth playerHealth;
    public ParticleSystem playerDeathParticle;

    [SerializeField]
    float horizontalMovementSpeed;
    [SerializeField]
    float verticalMovementSpeed;

    private SpriteRenderer playerSpriteRenderer;


    private bool coroutineActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveHorizontal();
        MoveVertical();
        PlayerShoot();
        PlayerDeath();

        if (EnemyManager.enemiesKilled == 0 && EnemyManager.goalReached)
            InitiateGodMode();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    private void MoveHorizontal()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector2.left * Time.deltaTime * horizontalMovementSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector2.right * Time.deltaTime * horizontalMovementSpeed);
        }
    }

    private void MoveVertical()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector2.up * Time.deltaTime * verticalMovementSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector2.down * Time.deltaTime * verticalMovementSpeed);
        }
    }

    IEnumerator ShootDelay()
    {
        coroutineActive = true;
        PlayerProjectile clone = Instantiate(playerProjectile, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.15f);
        coroutineActive = false;
    }

    private void PlayerShoot()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && !coroutineActive)
        {
            StartCoroutine(ShootDelay());
            //Debug.Log(clone.transform.position);
        }
    }

    private void PlayerDeath()
    {
        if (!playerHealth.isAlive)
        {
            ParticleSystem clone = Instantiate(playerDeathParticle, gameObject.transform.position, Quaternion.identity);
            playerHealth.isAlive = true;
            Destroy(gameObject);
        }
    }

    private void InitiateGodMode()
    {
        playerSpriteRenderer.color = new Color(239, 243, 0, 1);
    }
}
