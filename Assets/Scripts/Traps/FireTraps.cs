using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTraps : MonoBehaviour
{

    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay; // how many time before the activation when the player step on it
    [SerializeField] private float activationTime; //how long time the traps will be active

    private Animator anim;
    private SpriteRenderer spriteRend;
    private bool triggered; // when the trap get triggered
    private bool active; //when the trap is active and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRend.color = Color.red; // turn the sprite red to notify the player
        yield return new WaitForSeconds(activationDelay);

        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activationTime);
        
        active = false;
        anim.SetBool("activated", false);
        triggered = false;
    }
}
