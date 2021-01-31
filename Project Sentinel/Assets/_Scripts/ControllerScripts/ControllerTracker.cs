using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerTracker : MonoBehaviour
{
    private List<InputDevice> inputDevices, leftHandDevices, rightHandDevices;
    private Vector3 leftPos, rightPos;
    public GameObject leftHand, rightHand, referencePoint;
    public bool mobileReference;

    void Start()
    {
        //Detect VR controllers
        inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);
        leftHandDevices = new List<InputDevice>();
        rightHandDevices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            Debug.Log("Found 1 left-handed device: " + leftHandDevices[0].ToString());
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found multiple left-handed devices.");
        }
        else
        {
            Debug.Log("Found no left-handed devices.");
        }
        if (rightHandDevices.Count == 1)
        {
            Debug.Log("Found 1 right-handed device: " + rightHandDevices[0].ToString());
        }
        else if (rightHandDevices.Count > 1)
        {
            Debug.Log("Found multiple right-handed devices.");
        }
        else
        {
            Debug.Log("Found no right-handed devices.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion leftRot, rightRot;
        //get left controller transform

        if (mobileReference)
        {
            leftPos += referencePoint.transform.position;
            rightPos += referencePoint.transform.position;
        }

        if (leftHandDevices.Count > 0)
        {
            leftHandDevices[0].TryGetFeatureValue(CommonUsages.devicePosition, out leftPos);
            leftHandDevices[0].TryGetFeatureValue(CommonUsages.deviceRotation, out leftRot);
            leftHand.transform.position = leftPos;
            leftHand.transform.rotation = leftRot;
        }

        //get right controller transform
        if (rightHandDevices.Count > 0)
        {
            rightHandDevices[0].TryGetFeatureValue(CommonUsages.devicePosition, out rightPos);
            rightHandDevices[0].TryGetFeatureValue(CommonUsages.deviceRotation, out rightRot);
            rightHand.transform.position = rightPos;
            rightHand.transform.rotation = rightRot;
        }

    }

    public Vector3 getLeftPos()
    {
        return leftPos;
    }

    public Vector3 getRightPos()
    {
        return rightPos;
    }
}
