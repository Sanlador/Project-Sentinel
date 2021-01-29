using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject rig;
    private ControllerTracker tracker;
    private ControlReactor input;

    void Start()
    {
        tracker = rig.GetComponent<ControllerTracker>();
        input = rig.GetComponent<ControlReactor>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject getController(int controller)
    {
        if (controller == 1)
            return tracker.leftHand;
        else
            return tracker.rightHand;
    }


    /*proximityPress checks if a specified button (bool) has been pressed in proximity to an object
     * Uses a controller token to alias integers as button id's*/
    public int proximityPress(GameObject interactable, float maxDistance, int button, int controller)
    {
        float leftDist, rightDist;
        leftDist = Vector3.Distance(tracker.getLeftPos(), interactable.transform.position);
        rightDist = Vector3.Distance(tracker.getRightPos(), interactable.transform.position);

        if (controller == ControllerToken.leftController || controller == ControllerToken.controllerAgnostic)
        {
            if (leftDist <= maxDistance && getButtonByToken(button, ControllerToken.leftController))
            {
                return ControllerToken.leftController;
            }
        }
        if (controller == ControllerToken.rightController || controller == ControllerToken.controllerAgnostic)
        {
            if (rightDist <= maxDistance && getButtonByToken(button, ControllerToken.rightController))
            {
                return ControllerToken.rightController;
            }
        }
        

        return ControllerToken.none;
    }


    public bool getButtonByToken(int button, int controller)
    {
        if (controller == ControllerToken.leftController)
        {
            if (button == ControllerToken.primary)
            {
                return input.getPrimaryLeft();
            }
            else if (button == ControllerToken.secondary)
            {
                return input.getSecondaryLeft();
            }
            else if (button == ControllerToken.trigger)
            {
                return input.getTriggerLeft();
            }
            else if (button == ControllerToken.grip)
            {
                return input.getGripLeft();
            }
            else if (button == ControllerToken.menu)
            {
                return input.getMenuLeft();
            }
            else if (button == ControllerToken.joystickClick)
            {
                return input.getJoystickClickLeft();
            }
        }
        else if (controller == ControllerToken.rightController)
        {
            if (button == ControllerToken.primary)
            {
                return input.getPrimaryRight();
            }
            else if (button == ControllerToken.secondary)
            {
                return input.getSecondaryRight();
            }
            else if (button == ControllerToken.trigger)
            {
                return input.getTriggerRight();
            }
            else if (button == ControllerToken.grip)
            {
                return input.getGripRight();
            }
            else if (button == ControllerToken.menu)
            {
                return input.getMenuRight();
            }
            else if (button == ControllerToken.joystickClick)
            {
                return input.getJoystickClickRight();
            }
        }

        return false;
    }
}
