using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnnemyDamage
{
    [Header ("Spikehead attribut")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];


    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        // move spikehead to its final destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        // spikeHead sees directions in all direction
        foreach (Vector3 direction in directions)
        {
            Debug.DrawRay(transform.position, direction, Color.red); // for the log with Gizmos
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, playerLayer); // crée un laser invisble qui detecte uniquement le joueur via son layer

            // si le joueur est touché par le raycast et spikehead n'est pas en train d'attaquer
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = direction;
                checkTimer = 0;
            }
        }

    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; // right direction
        directions[1] = -transform.right * range; // left direction
        directions[2] = transform.up * range; // up direction
        directions[3] = -transform.up * range; // down direction
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        //stop Spikehead when it hit something
        Stop();

    }

    private void Stop()
    {
        destination = transform.position; // stop the spikehead by setting the destination by its position, so it doesn't move
        attacking = false;
    }
}
