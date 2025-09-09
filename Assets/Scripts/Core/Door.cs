using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            // if the player is in the left side of the door
            if (collision.transform.position.x < transform.position.x)
            {
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                nextRoom.GetComponent<Room>().ActivateRoom(false);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
            }

        }

        // move the camera room to room

        // if (collision.tag == "Player")
        // {
        //     if (collision.transform.position.x < transform.position.x)
        //         cam.MoveToNewRoom(nextRoom);
        //     else
        //         cam.MoveToNewRoom(previousRoom);

        // }
    }
}
