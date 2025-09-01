using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];

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
        // check if spikeHead sees directions 
        
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; // right direction
        directions[1] = - transform.right * range; // left direction
        directions[2] = transform.up * range; // up direction
        directions[3] = - transform.up * range; // down direction
    }
}
