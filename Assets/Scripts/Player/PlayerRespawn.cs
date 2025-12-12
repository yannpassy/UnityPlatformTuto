using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint; // we're stored the last checkpoint here
    private Health playerHealth;
    private UiManager uiManager;

    private void Awake() {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UiManager>();
        //currentCheckpoint.position = transform.position; // the location of the player at the start
    }

    // Call after the die animation of the player
    public void Respawn()
    {
        // if not checkpoint found => game over
        if(currentCheckpoint == null)
        {
            // Show game over screen

            return;
        }

        transform.position = currentCheckpoint.position; // set the player at the last checkpoint checked
        playerHealth.Ressurect();

        // For Room Camera only , not to the follow player camera
        //Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {   
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider 
            collision.GetComponent<Animator>().SetTrigger("appear"); // make the checkpoint appear
        }
    }
}
