using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFireballHolder : MonoBehaviour
{
    [SerializeField] Transform ennemy;

   private void Update()
    {
        transform.localScale = ennemy.localScale;
    }
}
