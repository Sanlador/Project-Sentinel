using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class boolButtonEvent : UnityEvent<bool> { }
public class floatButtonEvent : UnityEvent<float> { }
public class vectorButtonEvent : UnityEvent<Vector2> { }

public class ControlWatcher : MonoBehaviour
{
    public List<floatButtonEvent> floatButtonEventsLeft = new List<floatButtonEvent>();
    public List<floatButtonEvent> floatButtonEventsRight = new List<floatButtonEvent>();

    public List<boolButtonEvent> boolButtonEventsLeft = new List<boolButtonEvent>();
    public List<boolButtonEvent> boolButtonEventsRight = new List<boolButtonEvent>();

    public List<vectorButtonEvent> vectorButtonEventsLeft = new List<vectorButtonEvent>();
    public List<vectorButtonEvent> vectorButtonEventsRight = new List<vectorButtonEvent>();

    private List<bool> previousBoolButtonStatesLeft = new List<bool>();
    private List<bool> previousBoolButtonStatesRight = new List<bool>();

    private List<float> previousFloatButtonStatesLeft = new List<float>();
    private List<float> previousFloatButtonStatesRight = new List<float>();

    private List<Vector2> previousVectorButtonStatesLeft = new List<Vector2>();
    private List<Vector2> previousVectorButtonStatesRight = new List<Vector2>();

    private List<InputDevice> controllers;

    //list of buttons to be used in application; must be manually updated
    InputFeatureUsage<bool>[] boolButtons = 
    { 
        CommonUsages.primaryButton,
        CommonUsages.triggerButton, 
        CommonUsages.gripButton, 
        CommonUsages.menuButton
    };

    InputFeatureUsage<float>[] floatButtons =
    {
        CommonUsages.trigger,
        CommonUsages.grip
    };

    InputFeatureUsage<Vector2>[] vectorButtons =
    {
        CommonUsages.secondary2DAxis,
        CommonUsages.primary2DAxis
    };

    private void Awake()
    {
        for (int i = 0; i < boolButtons.Length; i++)
        {
            //must keep separate lists for left and right controllers to allow for differentiation
            boolButtonEventsLeft.Add(new boolButtonEvent());
            boolButtonEventsRight.Add(new boolButtonEvent());
            previousBoolButtonStatesLeft.Add(false);
            previousBoolButtonStatesRight.Add(false);
        }
        for (int i = 0; i < floatButtons.Length; i++)
        {
            floatButtonEventsLeft.Add(new floatButtonEvent());
            floatButtonEventsRight.Add(new floatButtonEvent());
            previousFloatButtonStatesLeft.Add(0f);
            previousFloatButtonStatesRight.Add(0f);
        }
        for (int i = 0; i < vectorButtons.Length; i++)
        {
            vectorButtonEventsLeft.Add(new vectorButtonEvent());
            vectorButtonEventsRight.Add(new vectorButtonEvent());
            previousVectorButtonStatesLeft.Add(new Vector2());
            previousVectorButtonStatesRight.Add(new Vector2());
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

    //bool buttons
    private void getButtonStates(List<bool> previousButtonStates, List<boolButtonEvent> buttonEvents, List<InputDevice> c, int itr)
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

    //float buttons
    private void getButtonStates(List<float> previousButtonStates, List<floatButtonEvent> buttonEvents, List<InputDevice> c, int itr)
    {
        bool tempState = false;
        float currentButtonState = 0f;
        foreach (var device in c)
        {
            tempState = device.TryGetFeatureValue(floatButtons[itr], out currentButtonState);
        }
        if (currentButtonState != previousButtonStates[itr])
        {
            buttonEvents[itr].Invoke(currentButtonState);
            previousButtonStates[itr] = currentButtonState;
        }

    }

    //Vector2 Buttons
    private void getButtonStates(List<Vector2> previousButtonStates, List<vectorButtonEvent> buttonEvents, List<InputDevice> c, int itr)
    {
        bool tempState = false;
        Vector2 currentButtonState = new Vector2();
        foreach (var device in c)
        {
            tempState = device.TryGetFeatureValue(vectorButtons[itr], out currentButtonState);
        }
        if (currentButtonState != previousButtonStates[itr])
        {
            buttonEvents[itr].Invoke(currentButtonState);
            previousButtonStates[itr] = currentButtonState;
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
            getButtonStates(previousBoolButtonStatesLeft, boolButtonEventsLeft, leftHandedControllers, i);
            getButtonStates(previousBoolButtonStatesRight, boolButtonEventsRight, rightHandedControllers, i);
        }
        for (int i = 0; i < floatButtons.Length; i++)
        {
            getButtonStates(previousFloatButtonStatesLeft, floatButtonEventsLeft, leftHandedControllers, i);
            getButtonStates(previousFloatButtonStatesRight, floatButtonEventsRight, rightHandedControllers, i);
        }
        for (int i = 0; i < vectorButtons.Length; i++)
        {
            getButtonStates(previousVectorButtonStatesLeft, vectorButtonEventsLeft, leftHandedControllers, i);
            getButtonStates(previousVectorButtonStatesRight, vectorButtonEventsRight, rightHandedControllers, i);
        }
    }
}