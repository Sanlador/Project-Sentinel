using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    public GameObject flightStick, manager;
    // Start is called before the first frame update
    public float movementSpeed = 1f;
    public float rotationSpeed = 20f;
    private bool mode = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x, y;
        x = flightStick.GetComponent<FlightStick>().getStickVector().x;
        y = flightStick.GetComponent<FlightStick>().getStickVector().y;
        float deltaTime = Time.deltaTime;

        if (manager.GetComponent<ControlManager>().getButtonByToken
            (ControllerToken.primary, ControllerToken.rightController))
        {
            mode = !mode;
            Debug.Log(mode);
        }
        if (mode)
        {
            Vector3 delta = new Vector3(deltaTime * x * movementSpeed, 0f, deltaTime * y * movementSpeed);
            gameObject.transform.position += delta;
        }
        else 
        {
            gameObject.transform.Rotate(y * deltaTime * rotationSpeed, 0, -x * deltaTime * rotationSpeed);
        }
    }
}
