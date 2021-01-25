using System.Collections;
using UnityEngine;

public class ControlReactor : MonoBehaviour
{
    public ControlWatcher watcher;
    private Coroutine rotator;
    

    void Start()
    {
        //initiate listener from watcher class
        
        //boolean buttons
        watcher.boolButtonEventsLeft[0].AddListener(onPrimaryButtonEventLeft);
        watcher.boolButtonEventsLeft[1].AddListener(onSecondaryButtonEventLeft);
        watcher.boolButtonEventsLeft[2].AddListener(onGripEventLeft);
        watcher.boolButtonEventsRight[0].AddListener(onPrimaryButtonEventRight);
        watcher.boolButtonEventsRight[1].AddListener(onSecondaryButtonEventRight);
        watcher.boolButtonEventsRight[2].AddListener(onGripEventRight);

        //float buttons
        watcher.floatButtonEventsLeft[0].AddListener(onLeftTriggerFloat);
        watcher.floatButtonEventsRight[0].AddListener(onRightTriggerFloat);
        watcher.floatButtonEventsLeft[1].AddListener(onLeftGripFloat);
        watcher.floatButtonEventsRight[1].AddListener(onRightGripFloat);

        //vector2 buttons
        watcher.vectorButtonEventsLeft[0].AddListener(onLeftJoystickVec2);
        watcher.vectorButtonEventsRight[0].AddListener(onRightJoystickVec2);
        watcher.vectorButtonEventsLeft[1].AddListener(onLeftJoystickVec2);
        watcher.vectorButtonEventsRight[1].AddListener(onRightJoystickVec2);
    }

    //Button Events

    //BOOLEAN BUTTON EVENTS
    public void onPrimaryButtonEventLeft(bool pressed)
    {
        Debug.Log("Left primary button pressed");

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
    }

    public void onSecondaryButtonEventRight(bool pressed)
    {
        //Debug.Log("Right trigger pressed");
    }

    public void onGripEventRight(bool pressed)
    {
        //Debug.Log("Right grip pressed");
    }


    // FLOAT BUTTON EVENTS
    public void onLeftGripFloat(float grip)
    {
        //Debug.Log("Left grip value: " + grip);
    }

    public void onRightGripFloat(float grip)
    {
        //Debug.Log("Right grip value: " + grip);
    }

    public void onLeftTriggerFloat(float grip)
    {
        //Debug.Log("Left trigger value: " + grip);
    }

    public void onRightTriggerFloat(float grip)
    {
        //Debug.Log("Right trigger value: " + grip);
    }

    //VECTOR2 BUTTON EVENTS
    public void onLeftJoystickVec2(Vector2 joy)
    {
        Debug.Log("Left joystick position: " + joy);
    }

    //VECTOR2 BUTTON EVENTS
    public void onRightJoystickVec2(Vector2 joy)
    {
        Debug.Log("Right joystick position: " + joy);
    }
}