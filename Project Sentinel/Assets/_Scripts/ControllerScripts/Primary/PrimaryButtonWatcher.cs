using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class ButtonEvent : UnityEvent<bool> { }

public class PrimaryButtonWatcher : MonoBehaviour
{
    public List<ButtonEvent> buttonEventsLeft = new List<ButtonEvent>();
    public List<ButtonEvent> buttonEventsRight = new List<ButtonEvent>();
    private List<bool> previousButtonStatesLeft = new List<bool>();
    private List<bool> previousButtonStatesRight = new List<bool>();
    private List<InputDevice> controllers;

    //list of buttons to be used in application; must be manually updated
    InputFeatureUsage<bool>[] boolButtons = 
    { 
        CommonUsages.primaryButton,
        CommonUsages.triggerButton, 
        CommonUsages.gripButton, 
        CommonUsages.menuButton
    };

    private void Awake()
    {
        for (int i = 0; i < boolButtons.Length; i++)
        {
            //must keep separate lists for left and right controllers to allow for differentiation
            buttonEventsLeft.Add(new ButtonEvent());
            buttonEventsRight.Add(new ButtonEvent());
            previousButtonStatesLeft.Add(false);
            previousButtonStatesRight.Add(false);
        }

        controllers = new List<InputDevice>();
    }

    //checks for abailable controllers
    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    //triggers on controller disabling; removes from list
    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        controllers.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
        {
            controllers.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (controllers.Contains(device))
            controllers.Remove(device);
    }

    private void getButtonStates(List<bool> previousButtonStates, List<ButtonEvent> buttonEvents, List<InputDevice> c, int itr)
    {
        bool tempState = false;
        foreach (var device in c)
        {
            bool currentButtonState = false;
            tempState = device.TryGetFeatureValue(boolButtons[itr], out currentButtonState) // did get a value
                        && currentButtonState // the value we got
                        || currentButtonState; // cumulative result from other controllers
        }

        if (tempState != previousButtonStates[itr]) // Button state changed since last frame
        {
            buttonEvents[itr].Invoke(tempState);
            previousButtonStates[itr] = tempState;
        }
    }

    void Update()
    {
        //discount non-VR controllers

        //set filtering parameters
        var leftCharacteristics =
            UnityEngine.XR.InputDeviceCharacteristics.HeldInHand |
            UnityEngine.XR.InputDeviceCharacteristics.Left |
            UnityEngine.XR.InputDeviceCharacteristics.Controller;
        var rightCharacteristics =
            UnityEngine.XR.InputDeviceCharacteristics.HeldInHand |
            UnityEngine.XR.InputDeviceCharacteristics.Right |
            UnityEngine.XR.InputDeviceCharacteristics.Controller;


        //split into lists based on left and right-handed controllers
        List<InputDevice> leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        List<InputDevice> rightHandedControllers = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(leftCharacteristics, leftHandedControllers);
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rightCharacteristics, rightHandedControllers);

        //checks the state of each button from button list
        for (int i = 0; i < boolButtons.Length; i++)
        {
            getButtonStates(previousButtonStatesLeft, buttonEventsLeft, leftHandedControllers, i);
            getButtonStates(previousButtonStatesRight, buttonEventsRight, rightHandedControllers, i);
        }
        
    }
}