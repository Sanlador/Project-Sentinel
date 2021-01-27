using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStick : MonoBehaviour
{
    public GameObject rig, left, right;
    public float distanceThreshold;
    public bool testMode;

    private GameObject controller;
    private ControlManager manager;
    private bool isGripped = false;
    private int grippingController;

    // Start is called before the first frame update
    void Start()
    {
        manager = rig.GetComponent<ControlManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGripped && !(manager.proximityPress(gameObject, distanceThreshold, ControllerToken.grip,
            grippingController) == grippingController))
        {
            Debug.Log("Test");
            isGripped = false;
            gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
            if (!testMode)
            {
                left.SetActive(true);
                right.SetActive(true);
            }
            
        }
        else if (isGripped)
        {
            gameObject.transform.LookAt(controller.transform);
        }
        else
        {
            if (!isGripped &&
            (manager.proximityPress(gameObject, distanceThreshold, ControllerToken.grip,
            ControllerToken.controllerAgnostic) > 0))
            {
                isGripped = true;
                grippingController = manager.proximityPress(gameObject, distanceThreshold, ControllerToken.grip,
                ControllerToken.controllerAgnostic);

                if (grippingController == ControllerToken.leftController)
                {
                    controller = manager.rig.GetComponent<ControllerTracker>().leftHand;
                    if (!testMode)
                        left.SetActive(false);
                }
                else
                {
                    controller = manager.rig.GetComponent<ControllerTracker>().rightHand;
                    if (!testMode)
                        right.SetActive(false);
                }
            }
        }

        
    }
}
