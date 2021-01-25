using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject rig;
    private ControllerTracker tracker;
    private ControlReactor input;

    // Start is called before the first frame update
    void Start()
    {
        tracker = rig.GetComponent<ControllerTracker>();
        input = rig.GetComponent<ControlReactor>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ControllerToken proximityPress(GameObject interactable, float maxDistance, ControllerToken button)
    {
        ControllerToken left = new ControllerToken();
        ControllerToken right = new ControllerToken();
        ControllerToken none = new ControllerToken();
        left.c = ControllerToken.controller.left;
        right.c = ControllerToken.controller.right;
        none.c = ControllerToken.controller.none;
        float leftDist, rightDist;
        leftDist = Vector3.Distance(tracker.getLeftPos(), interactable.transform.position);
        rightDist = Vector3.Distance(tracker.getRightPos(), interactable.transform.position);

        if (leftDist <= maxDistance && getButtonByToken(button, left))
        {
            return left;
        }
        else if (rightDist <= maxDistance && getButtonByToken(button, right))
        {
            return right;
        }

        return none;
    }


    private bool getButtonByToken(ControllerToken token, ControllerToken c)
    {
        return true;
    }
}
