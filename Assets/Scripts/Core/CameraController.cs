using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Update()
    {
        //Room Camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);


        //Follow Player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Lerp(lookAhead, aheadDistance * MathF.Sign(player.localScale.x), cameraSpeed * Time.deltaTime); // you can use Mathf.Lerp() instead with lower f and not upper F
        
    }

    // code source of Mathf.Lerp
    public float Lerp(float start, float end, float time)
    {
        return start + (end - start) * Mathf.Clamp01(time);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x + 1;
    }

}
