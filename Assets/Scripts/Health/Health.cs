using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")] // in inspector, see the header
    [SerializeField] private float maxHealth;
    public float currentHealth { get; private set; } // used in HealthBar, we can get the value from everywhere but cannot set it outside this class
    private Animator anim;
    private bool dead;

    [Header("iFrame")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numberFlash;
    private SpriteRenderer spriteRend;
    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
            
        }
    }

    public void GainLife(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }

    // IEnumerator == asynchrone
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true); // ignore la colistion entre le player et les ennemies via leur layer
        for (int i = 0; i < numberFlash; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration/(numberFlash *2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration/(numberFlash *2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false); // reactive la colision

    }

    // just for testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GainLife(1);
        }
    }
}
