using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float projectileMovementSpeed;

    private bool hitEnemy = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FireProjectile();
        DestroyProjectile();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hitEnemy = true;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void FireProjectile()
    {
        //Debug.Log(gameObject.transform);
        rb.AddRelativeForce(Vector2.up * Time.deltaTime * projectileMovementSpeed); 
    }

    private void DestroyProjectile()
    {
        if (!IsProjectileOnScreen() || hitEnemy)
        {
            Destroy(gameObject);
        }
    }

    private bool IsProjectileOnScreen()
    {
        Camera mainCamera = FindObjectOfType<Camera>();
        Vector2 screenPoint = mainCamera.WorldToViewportPoint(gameObject.transform.position);
        return screenPoint.x > 0 &&
               screenPoint.x < 1 &&
               screenPoint.y > 0 &&
               screenPoint.y < 1;
    }
}
