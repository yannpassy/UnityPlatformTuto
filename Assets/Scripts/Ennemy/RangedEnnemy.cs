using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnnemy : MonoBehaviour
{
    [Header ("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header ("Collider parameters")]
    [SerializeField] private float colliderRange;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header ("Layer parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
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
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack"); // launch attack and apply damage

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

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderRange,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    
    private void rangedAttack() // Called during rangedAttack Animation
    {
        cooldownTimer = 0;
        //shoot projectile
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnnemyProjectile>().LaunchProjectile();
    }

    private int FindFireball()
    {
        for(int i = 0; i< fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
