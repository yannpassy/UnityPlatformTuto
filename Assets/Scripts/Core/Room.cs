using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] ennemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        // save the initial position of all ennemies
        initialPosition = new Vector3[ennemies.Length];
        for (int i = 0; i < ennemies.Length; i++)
        {
            if (ennemies[i] != null)
            {
                initialPosition[i] = ennemies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool _status)
    {
        for (int i = 0; i < ennemies.Length; i++)
        {
            if (ennemies[i] != null)
            {
                ennemies[i].SetActive(_status);
                ennemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
