using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStick : MonoBehaviour
{
    public GameObject rig, left, right;
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
        if (isGripped && !(manager.proximityPress(gameObject, 0.1f, ControllerToken.grip,
            ControllerToken.controllerAgnostic) > 0))
        {
            isGripped = false;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            left.SetActive(true);
            right.SetActive(true);
        }
        else if (isGripped)
        {
            gameObject.transform.rotation = controller.transform.rotation;
        }
        

        if (!isGripped && 
            (manager.proximityPress(gameObject, 1f, ControllerToken.grip, 
            ControllerToken.controllerAgnostic) > 0))
        {
            isGripped = true;
            grippingController = manager.proximityPress(gameObject, 0.1f, ControllerToken.grip,
            ControllerToken.controllerAgnostic);

            if (grippingController == ControllerToken.leftController)
            {
                controller = manager.rig.GetComponent<ControllerTracker>().leftHand;
                left.SetActive(false);
            }
            else
            {
                controller = manager.rig.GetComponent<ControllerTracker>().rightHand;
                right.SetActive(false);
            }
        }
    }
}
