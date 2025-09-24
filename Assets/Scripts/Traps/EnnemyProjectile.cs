using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class EnnemyProjectile : EnnemyDamage // inheritence
{
    [Header("Projectile")]
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float arrowLifeTime;
    public void ActivateArrow()
    {
        arrowLifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // move the arrow
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        arrowLifeTime += Time.deltaTime;
        if (arrowLifeTime > resetTime)
            gameObject.SetActive(false);

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
