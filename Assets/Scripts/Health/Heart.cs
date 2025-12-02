using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float heartValue;
    [Header ("SFX")]
    [SerializeField] private AudioClip pickupSound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().GainLife(heartValue);
            gameObject.SetActive(false);
        }
    }
}
