using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Ennemy")]
    [SerializeField] private Transform ennemy;

    [Header("Movement parameter")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    private void Awake() {
        initScale = ennemy.localScale;
    }

    private void Update() {
        if (movingLeft)
        {
            moveInDirection(-1);
        }
        else
        {
            moveInDirection(1);
        }
        
    }
    private void moveInDirection(int _direction)
    {
        // Make ennemy face direction
        ennemy.localScale = new Vector3(Mathf.Abs(ennemy.localScale.x) * _direction,
            ennemy.localScale.y, ennemy.localScale.z);

        //Move in that direction
        ennemy.position = new Vector3(ennemy.position.x + Time.deltaTime * speed * _direction,
            ennemy.position.y, ennemy.position.z);
    }
}
