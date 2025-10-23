using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        hit = false;
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit)
        {
            return;
        }

        // the fireball moves
        float mouvementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(mouvementSpeed, 0, 0);

        // prevent infinite movement of the fireball (in case if it doesn't hit a something)
        lifeTime += Time.deltaTime;
        if (lifeTime > 5) Deactivate();  //5 is chosen by default, can be any number
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.CompareTag("Ennemies"))
            collision.GetComponent<Health>().TakeDamage(1);
            
    }

    public void LaunchFireball(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        // fireball direction
        float localScaleX = transform.localScale.x;
        localScaleX = (Mathf.Sign(localScaleX) != _direction) ? -localScaleX : localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // also call at the end of the animation explode
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
