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
    private Animator anim;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }
    public void LaunchProjectile()
    {
        projectileLifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // move the arrow
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        projectileLifeTime += Time.deltaTime;
        if (projectileLifeTime > resetTime)
            gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

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
