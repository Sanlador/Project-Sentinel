using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStick : MonoBehaviour
{
    public GameObject rig;
    private GameObject left, right;
    public float distanceThreshold;
    public bool testMode;

    private GameObject controller;
    private ControlManager manager;
    private bool isGripped = false;
    private int grippingController;
    private Vector2 stickVector;

    // Start is called before the first frame update
    void Start()
    {
        manager = rig.GetComponent<ControlManager>();
        left = manager.getController(ControllerToken.leftController);
        right = manager.getController(ControllerToken.rightController);
        stickVector = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGripped && !(manager.proximityPress(gameObject, distanceThreshold, ControllerToken.grip,
            grippingController) == grippingController))
        {
            isGripped = false;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            stickVector.x = 0f;
            stickVector.y = 0f;
            if (!testMode)
            {
                left.SetActive(true);
                right.SetActive(true);
            }
        }
        else if (isGripped)
        {
            float x, y, z;
            gameObject.transform.LookAt(controller.transform, Vector3.back);
            gameObject.transform.Rotate(90, 0, 0);

            Vector3 direction = gameObject.transform.rotation * Vector3.up;
            stickVector.x = direction.x;
            stickVector.y = direction.z;
            //Debug.Log(direction);
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

    public Vector2 getStickVector()
    {
        return stickVector;
    }
}
