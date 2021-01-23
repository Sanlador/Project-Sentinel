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
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        Debug.Log("Primary button pressed");
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
}