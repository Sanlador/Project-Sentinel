using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class ButtonEvent : UnityEvent<bool> { }

public class PrimaryButtonWatcher : MonoBehaviour
{
    public List<ButtonEvent> buttonEvents = new List<ButtonEvent>();
    private List<bool> previousButtonStates = new List<bool>();
    private List<InputDevice> controllers;

    InputFeatureUsage<bool>[] buttons = { CommonUsages.primaryButton, CommonUsages.triggerButton,
     CommonUsages.gripButton, CommonUsages.menuButton};

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonEvents.Add(new ButtonEvent());
            previousButtonStates.Add(false);
        }

        controllers = new List<InputDevice>();
    }

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

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

    void Update()
    {
        
        for (int i = 0; i < buttons.Length; i++)
        {
            bool tempState = false;
            foreach (var device in controllers)
            {
                bool currentButtonState = false;
                tempState = device.TryGetFeatureValue(buttons[i], out currentButtonState) // did get a value
                            && currentButtonState // the value we got
                            || currentButtonState; // cumulative result from other controllers
            }

            if (tempState != previousButtonStates[i]) // Button state changed since last frame
            {
                buttonEvents[i].Invoke(tempState);
                previousButtonStates[i] = tempState;
            }
        }
        
    }
}