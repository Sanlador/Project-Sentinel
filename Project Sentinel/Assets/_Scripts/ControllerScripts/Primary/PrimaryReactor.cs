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
        watcher.boolButtonEventsLeft[0].AddListener(onPrimaryButtonEventLeft);
        watcher.boolButtonEventsLeft[1].AddListener(onSecondaryButtonEventLeft);
        watcher.boolButtonEventsLeft[2].AddListener(onGripEventLeft);
        watcher.boolButtonEventsRight[0].AddListener(onPrimaryButtonEventRight);
        watcher.boolButtonEventsRight[1].AddListener(onSecondaryButtonEventRight);
        watcher.boolButtonEventsRight[2].AddListener(onGripEventRight);

        watcher.floatButtonEventsLeft[0].AddListener(onLeftTriggerFloat);
        watcher.floatButtonEventsRight[0].AddListener(onRightTriggerFloat);
        watcher.floatButtonEventsLeft[1].AddListener(onLeftGripFloat);
        watcher.floatButtonEventsRight[1].AddListener(onRightGripFloat);
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

    public void onLeftGripFloat(float grip)
    {
        Debug.Log("Left grip value: " + grip);
    }

    public void onRightGripFloat(float grip)
    {
        Debug.Log("Right grip value: " + grip);
    }

    public void onLeftTriggerFloat(float grip)
    {
        Debug.Log("Left trigger value: " + grip);
    }

    public void onRightTriggerFloat(float grip)
    {
        Debug.Log("Right trigger value: " + grip);
    }
}