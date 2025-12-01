using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnnemy : MonoBehaviour
{
    [Header ("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private AudioClip attackSound;

    [Header ("Collider parameters")]
    [SerializeField] private float colliderRange;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Layer parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //reference
    private Animator anim;
    private Health playerHealth;
    private EnnemyPatrol ennemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ennemyPatrol = GetComponentInParent<EnnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (IsPlayerInSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0) 
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack"); // launch attack and apply damage
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if (ennemyPatrol != null)
            ennemyPatrol.enabled = !IsPlayerInSight(); // if the ennemy don't see the player, it keep patrolling

    }

    private bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderRange,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderRange,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // used in the animation melee attack
    private void DamagePlayer()
    {
        if (IsPlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
