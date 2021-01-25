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

    
}
