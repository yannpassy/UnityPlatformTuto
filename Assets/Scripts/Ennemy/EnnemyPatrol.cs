using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Ennemy")]
    [SerializeField] private Transform ennemy;
    private void moveInDirection(int _direction)
    {
        // Make ennemy face direction

        //Move in that direction
    }
}
