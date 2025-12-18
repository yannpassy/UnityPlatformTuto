using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; // sound when the arrow change position
    [SerializeField] private AudioClip OptionSelectedSound; // sound when an option is selected
    private RectTransform rect;
    private int currentPosition;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(currentPosition < 0)
        {
            currentPosition = options.Length -1;
        }
        else if(currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }
}
