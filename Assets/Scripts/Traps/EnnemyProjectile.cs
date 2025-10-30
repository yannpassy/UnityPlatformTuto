using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class EnnemyProjectile : EnnemyDamage // inheritence
{
    [Header("Projectile")]
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float projectileLifeTime;
    private BoxCollider2D collider;
    private Animator anim;
    private bool hit;

    private void Start()
    {
        hit = false;
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    public void LaunchProjectile()
    {

        gameObject.SetActive(true);
        hit = false;
        collider.enabled = true;
        projectileLifeTime = 0;
    }

    private void Update()
    {
        if (hit) // stop the movement of the pojectile when hit
        {
            return;
        }
        // move the arrow
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        projectileLifeTime += Time.deltaTime;
        if (projectileLifeTime > resetTime)
            gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
        
        collider.enabled = false;

        if (anim != null)
        {
            anim.SetTrigger("explode"); // fireball projectile
        }
        else
        {
            Deactivate(); // arrow projectile
        }

        
    }
    
    // also call at the end of the animation explode
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
