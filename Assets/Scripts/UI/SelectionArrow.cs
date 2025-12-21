using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Update()
    {
        // change postion of the arrow
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        // interact with the option
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if( _change != 0)
            SoundManager.instance.PlaySound(changeSound);

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

    private void Interact()
    {
        SoundManager.instance.PlaySound(OptionSelectedSound);

        // Access of the button of the coponent and call it's function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
