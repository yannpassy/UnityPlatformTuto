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

    private void Update()
    {
        // move spikehead to its final destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
    }
}
