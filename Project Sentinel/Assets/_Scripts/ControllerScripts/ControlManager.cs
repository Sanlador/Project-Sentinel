using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject rig;
    private ControllerTracker tracker;
    private ControlReactor input;
    private bool primaryLeftState = false, primaryRightState = false, secondaryLeftState = false,
        secondaryRightState = false, triggerLeftState = false, triggerRightState = false,
        gripLeftState = false, gripRightState = false, menuLeftState = false,
        menuRightState = false, joystickClickLeftState = false, joystickClickRightState = false,

        primaryLeftChanged = true, primaryRightChanged = true, secondaryLeftChanged = true,
        secondaryRightChanged = true, triggerLeftChanged = true, triggerRightChanged = true,
        gripLeftChanged = true, gripRightChanged = true, menuLeftChanged = true,
        menuRightChanged = true, joystickClickLeftChanged = true, joystickClickRightChanged = true;

    void Start()
    {
        tracker = rig.GetComponent<ControllerTracker>();
        input = rig.GetComponent<ControlReactor>();
    }

    // Update is called once per frame
    void Update()
    {
        getButtonStates();
    }


    //checks every frame to see if any given button state has changed, in order to determine if a
    //button toggle has occured
    private void getButtonStates()
    {
        if (primaryLeftState != input.getPrimaryLeft())
            primaryLeftChanged = true;
        else
            primaryLeftChanged = false;
        primaryLeftState = input.getPrimaryLeft();


        if (primaryRightState != input.getPrimaryRight())
            primaryRightChanged = true;
        else
            primaryRightChanged = false;
        primaryRightState = input.getPrimaryRight();


        if (secondaryLeftState != input.getSecondaryLeft())
            secondaryLeftChanged = true;
        else
            secondaryLeftChanged = false;
        secondaryLeftState = input.getSecondaryLeft();


        if (secondaryRightState != input.getSecondaryRight())
            secondaryRightChanged = true;
        else
            secondaryRightChanged = false;
        secondaryRightState = input.getSecondaryRight();


        if (triggerLeftState != input.getTriggerLeft())
            triggerLeftChanged = true;
        else
            triggerLeftChanged = false;
        triggerLeftState = input.getTriggerLeft();


        if (triggerRightState != input.getTriggerRight())
            triggerRightChanged = true;
        else
            triggerRightChanged = false;
        triggerRightState = input.getTriggerRight();


        if (gripLeftState != input.getGripLeft())
            gripLeftChanged = true;
        else
            gripLeftChanged = false;
        gripLeftState = input.getGripLeft();


        if (gripRightState != input.getGripRight())
            gripRightChanged = true;
        else
            gripRightChanged = false;
        gripRightState = input.getGripRight();


        if (menuLeftState != input.getMenuLeft())
            menuLeftChanged = true;
        else
            menuLeftChanged = false;
        menuLeftState = input.getMenuLeft();


        if (menuRightState != input.getMenuRight())
            menuRightChanged = true;
        else
            menuRightChanged = false;
        menuRightState = input.getMenuRight();


        if (joystickClickLeftState != input.getJoystickClickLeft())
            joystickClickLeftChanged = true;
        else
            joystickClickLeftChanged = false;
        joystickClickLeftState = input.getJoystickClickRight();


        if (joystickClickRightState != input.getJoystickClickRight())
            joystickClickRightChanged = true;
        else
            joystickClickRightChanged = false;
        joystickClickRightState = input.getJoystickClickRight();
    }

    

    /*proximityPress checks if a specified button (bool) has been pressed in proximity to an object
     * Uses a controller token to alias integers as button id's*/
    public int proximityPress(GameObject interactable, float maxDistance, int button, int controller,
        bool toggle = false)
    {
        float leftDist, rightDist;
        leftDist = Vector3.Distance(tracker.getLeftPos(), interactable.transform.position);
        rightDist = Vector3.Distance(tracker.getRightPos(), interactable.transform.position);

        if (controller == ControllerToken.leftController || controller == ControllerToken.controllerAgnostic)
        {
            if (leftDist <= maxDistance && getButtonByToken(button, ControllerToken.leftController, toggle))
            {
                return ControllerToken.leftController;
            }
        }
        if (controller == ControllerToken.rightController || controller == ControllerToken.controllerAgnostic)
        {
            if (rightDist <= maxDistance && getButtonByToken(button, ControllerToken.rightController, toggle))
            {
                return ControllerToken.rightController;
            }
        }
        

        return ControllerToken.none;
    }


    public bool getButtonByToken(int button, int controller, bool toggle)
    {
        if (controller == ControllerToken.leftController)
        {
            if (button == ControllerToken.primary)
            {
                if (!toggle || primaryLeftChanged)
                    return input.getPrimaryLeft();
            }
            else if (button == ControllerToken.secondary)
            {
                if (!toggle || secondaryLeftChanged)
                    return input.getSecondaryLeft();
            }
            else if (button == ControllerToken.trigger)
            {
                if (!toggle || triggerLeftChanged)
                    return input.getTriggerLeft();
            }
            else if (button == ControllerToken.grip)
            {
                if (!toggle || gripLeftChanged)
                    return input.getGripLeft();
            }
            else if (button == ControllerToken.menu)
            {
                if (!toggle || menuLeftChanged)
                    return input.getMenuLeft();
            }
            else if (button == ControllerToken.joystickClick)
            {
                if (!toggle || joystickClickLeftChanged)
                    return input.getJoystickClickLeft();
            }
        }
        else if (controller == ControllerToken.rightController)
        {
            if (button == ControllerToken.primary && (!toggle || primaryRightState))
            {
                if (!toggle || primaryRightChanged)
                    return input.getPrimaryRight();
            }
            else if (button == ControllerToken.secondary)
            {
                if (!toggle || secondaryRightChanged)
                    return input.getSecondaryRight();
            }
            else if (button == ControllerToken.trigger)
            {
                if (!toggle || triggerRightChanged)
                    return input.getTriggerRight();
            }
            else if (button == ControllerToken.grip)
            {
                if (!toggle || gripRightChanged)
                    return input.getGripRight();
            }
            else if (button == ControllerToken.menu)
            {
                if (!toggle || menuRightChanged)
                    return input.getMenuRight();
            }
            else if (button == ControllerToken.joystickClick)
            {
                if (!toggle || joystickClickRightChanged)
                    return input.getJoystickClickRight();
            }
        }
        
        return false;
    }

}
