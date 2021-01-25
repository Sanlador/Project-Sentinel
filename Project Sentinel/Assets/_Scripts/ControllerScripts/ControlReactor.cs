using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class ControlReactor : MonoBehaviour
{
    public ControlWatcher watcher;
    public GameObject controllerRig;
    private GameObject leftHand, rightHand;

    //controller inputs
    private bool primaryLeft, primaryRight, secondaryLeft, secondaryRight, gripLeft, gripRight, 
        triggerLeft, triggerRight, menuLeft, menuRight, joystickClickLeft, joystickClickRight;

    private float triggerPressLeft, triggerPressRight, gripPressLeft, gripPressRight;

    private Vector2 joystickLeft, joystickRight;


    void Start()
    {
        //SET UP EVENT LISTENER
        //initiate listener from watcher class
        
        //boolean buttons
        watcher.boolButtonEventsLeft[0].AddListener(onPrimaryButtonEventLeft);
        watcher.boolButtonEventsRight[0].AddListener(onPrimaryButtonEventRight);

        watcher.boolButtonEventsLeft[1].AddListener(onSecondaryButtonEventLeft);
        watcher.boolButtonEventsRight[1].AddListener(onSecondaryButtonEventRight);

        watcher.boolButtonEventsLeft[2].AddListener(onTriggerEventLeft);
        watcher.boolButtonEventsRight[2].AddListener(onTriggerEventRight);

        watcher.boolButtonEventsLeft[3].AddListener(onGripEventLeft);
        watcher.boolButtonEventsRight[3].AddListener(onGripEventRight);

        watcher.boolButtonEventsLeft[4].AddListener(onMenuEventLeft);
        watcher.boolButtonEventsRight[4].AddListener(onMenuEventRight);

        watcher.boolButtonEventsLeft[5].AddListener(onJoystickClickEventLeft);
        watcher.boolButtonEventsRight[5].AddListener(onJoystickClickEventRight);


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

    //INPUT RETURN FUNCTIONS


    //boolean inputs
    public bool getPrimaryLeft()
    {
        return primaryLeft;
    }

    public bool getPrimaryRight()
    {
        return primaryRight;
    }

    public bool getSecondaryLeft()
    {
        return secondaryLeft;
    }

    public bool getSecondaryRight()
    {
        return secondaryRight;
    }

    public bool getTriggerLeft()
    {
        return triggerLeft;
    }

    public bool getTriggerRight()
    {
        return triggerRight;
    }

    public bool getGripLeft()
    {
        return gripLeft;
    }

    public bool getGripRight()
    {
        return gripRight;
    }

    public bool getMenuLeft()
    {
        return menuLeft;
    }

    public bool getMenuRight()
    {
        return menuRight;
    }

    public bool getJoystickClickLeft()
    {
        return joystickClickLeft;
    }

    public bool getJoystickCLickRight()
    {
        return joystickClickRight;
    }

    
    //float inputs
    public float getTriggerPressLeft()
    {
        return triggerPressLeft;
    }

    public float getTriggerPressRight()
    {
        return triggerPressRight;
    }

    public float getGripPressLeft()
    {
        return gripPressLeft;
    }

    public float getGrupPressRight()
    {
        return gripPressRight;
    }


    //Vector2 inputs
    public Vector2 getJoystickLeft()
    {
        return joystickLeft;
    }

    public Vector2 getJoystickRight()
    {
        return joystickRight;
    }

    //BUTTON EVENTS

    //BOOLEAN BUTTON EVENTS
    public void onPrimaryButtonEventLeft(bool pressed)
    {
        //Debug.Log("Left primary button pressed");
        primaryLeft = pressed;

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

    public void onPrimaryButtonEventRight(bool pressed)
    {
        //Debug.Log("Right primary button pressed");
        primaryRight = pressed;
    }

    public void onSecondaryButtonEventLeft(bool pressed)
    {
        //Debug.Log("Left trigger pressed");
        secondaryLeft = pressed;
    }

    public void onSecondaryButtonEventRight(bool pressed)
    {
        //Debug.Log("Right trigger pressed");
        secondaryRight = pressed;
    }

    void onTriggerEventLeft(bool pressed)
    {
        triggerLeft = pressed;
    }

    void onTriggerEventRight(bool pressed)
    {
        triggerRight = pressed;
    }

    public void onGripEventLeft(bool pressed)
    {
        //Debug.Log("Left grip pressed");
        gripLeft = pressed;
    }

    public void onGripEventRight(bool pressed)
    {
        //Debug.Log("Right grip pressed");
        gripRight = pressed;
    }

    void onMenuEventLeft(bool pressed)
    {
        menuLeft = pressed;
    }

    void onMenuEventRight(bool pressed)
    {
        menuRight = pressed;
    }

    void onJoystickClickEventLeft(bool pressed)
    {
        joystickClickLeft = pressed;
    }

    void onJoystickClickEventRight(bool pressed)
    {
        joystickClickRight = pressed;
    }



    // FLOAT BUTTON EVENTS

    public void onLeftTriggerFloat(float trigger)
    {
        //Debug.Log("Left trigger value: " + grip);
        triggerPressLeft = trigger;
    }

    public void onRightTriggerFloat(float trigger)
    {
        //Debug.Log("Right trigger value: " + grip);
        triggerPressRight = trigger;
    }

    public void onLeftGripFloat(float grip)
    {
        //Debug.Log("Left grip value: " + grip);
        gripPressLeft = grip;
    }

    public void onRightGripFloat(float grip)
    {
        //Debug.Log("Right grip value: " + grip);
        gripPressRight = grip;
    }

    //VECTOR2 BUTTON EVENTS
    public void onLeftJoystickVec2(Vector2 joy)
    {
        //Debug.Log("Left joystick position: " + joy);
        joystickLeft = joy;
    }

    public void onRightJoystickVec2(Vector2 joy)
    {
        //Debug.Log("Right joystick position: " + joy);
        joystickRight = joy;
    }


}