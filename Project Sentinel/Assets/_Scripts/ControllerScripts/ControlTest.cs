using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTest : MonoBehaviour
{
    public GameObject control;
    private ControlManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = control.GetComponent<ControlManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(manager.proximityPress(gameObject, .1f, ControllerToken.primary, ControllerToken.controllerAgnostic));
    }
}
