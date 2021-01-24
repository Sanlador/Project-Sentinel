using System.Collections;
using UnityEngine;

public class PrimaryReactor : MonoBehaviour
{
    public PrimaryButtonWatcher watcher;
    public bool IsPressed = false; // used to display button state in the Unity Inspector window
    private Coroutine rotator;
    

    void Start()
    {
        //initiate listener from watcher class
        watcher.buttonEventsLeft[0].AddListener(onPrimaryButtonEventLeft);
        watcher.buttonEventsLeft[1].AddListener(onSecondaryButtonEventLeft);
        watcher.buttonEventsLeft[2].AddListener(onGripEventLeft);
        watcher.buttonEventsRight[0].AddListener(onPrimaryButtonEventRight);
        watcher.buttonEventsRight[1].AddListener(onSecondaryButtonEventRight);
        watcher.buttonEventsRight[2].AddListener(onGripEventRight);
    }

    public void onPrimaryButtonEventLeft(bool pressed)
    {
        Debug.Log("Left primary button pressed");
        IsPressed = pressed;

        //coroutine example code
        /*
         * Sets up coroutine to run asynchronously, allowing for multi-frame functions
         * if (rotator != null)
            StopCoroutine(rotator);
        if (pressed)
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, onRotation)); 
        else
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, offRotation));*/
    }

    public void onSecondaryButtonEventLeft(bool pressed)
    {
        Debug.Log("Left trigger pressed");
    }

    public void onGripEventLeft(bool pressed)
    {
        Debug.Log("Left grip pressed");
    }

    public void onPrimaryButtonEventRight(bool pressed)
    {
        Debug.Log("Right primary button pressed");
        IsPressed = pressed;

        //coroutine example code
        /*
         * Sets up coroutine to run asynchronously, allowing for multi-frame functions
         * if (rotator != null)
            StopCoroutine(rotator);
        if (pressed)
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, onRotation)); 
        else
            rotator = StartCoroutine(AnimateRotation(this.transform.rotation, offRotation));*/
    }

    public void onSecondaryButtonEventRight(bool pressed)
    {
        Debug.Log("Right trigger pressed");
    }

    public void onGripEventRight(bool pressed)
    {
        Debug.Log("Right grip pressed");
    }
}