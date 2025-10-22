using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Ennemy")]
    [SerializeField] private GameObject ennemy;

    [Header("Movement parameter")]
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;
    private float idleTimer; 
    private Animator anim;
    private bool movingLeft;

    private void Awake()
    {
        anim = ennemy.GetComponent<Animator>();
    }
    
    // is disable when the ennemy detect the player
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (ennemy.transform.position.x >= leftEdge.position.x ) // si il n'a pas atteint le leftEdge
                moveInDirection(-1);
            else
                ChangeDirection();
        }
        else // moving right
        {
            if (ennemy.transform.position.x <= rightEdge.position.x) // si il n'a pas atteint le rightEdge
                moveInDirection(1);
            
            else
                ChangeDirection();

        }

    }
    
    // movingLeft true => false et inversement
    private void ChangeDirection()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = movingLeft ? false : true;   // movingLeft =!movingLeft
    }

    private void moveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        // Make ennemy face direction (scale)
        ennemy.transform.localScale = new Vector3(Mathf.Abs(ennemy.transform.localScale.x) * _direction,
            ennemy.transform.localScale.y, ennemy.transform.localScale.z);

        //Move in that direction (position)
        ennemy.transform.position = new Vector3(ennemy.transform.position.x + Time.deltaTime * speed * _direction,
            ennemy.transform.position.y, ennemy.transform.position.z);
    }
}
