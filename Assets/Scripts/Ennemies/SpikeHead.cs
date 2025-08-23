using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnnemyDamage
{
    [SerializeField] private float speed;
    private Vector3 destination;

    private void Update()
    {
        transform.Translate(destination * Time.deltaTime * speed);
    }
}
