using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    public GameObject flightStick, manager;
    // Start is called before the first frame update
    public float speedLimit = 1f;
    public float drag = 0.03f;
    public float acc = .05f;
    
    private float xSpeed = 0f, ySpeed = 0f, speed = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get joystick vector
        float x, y;
        x = flightStick.GetComponent<FlightStick>().getStickVector().x;
        y = flightStick.GetComponent<FlightStick>().getStickVector().y;


        xSpeed += x * acc;
        ySpeed += y * acc;
        speed = Mathf.Sqrt(xSpeed * xSpeed + ySpeed * ySpeed);

        if (speed > 0f)
        {
            if (Mathf.Abs(xSpeed) > 0f)
            {
                if (xSpeed > 0f)
                {
                    xSpeed -= drag;
                    if (xSpeed < 0f)
                        xSpeed = 0f;
                }
                else
                {
                    xSpeed += drag;
                    if (xSpeed > 0f)
                        xSpeed = 0f;
                }
            }
            if (Mathf.Abs(ySpeed) > 0f)
            {
                if (ySpeed > 0f)
                {
                    ySpeed -= drag;
                    if (ySpeed < 0f)
                        ySpeed = 0f;
                }
                else
                {
                    ySpeed += drag;
                    if (ySpeed > 0f)
                        ySpeed = 0f;
                }
            }
        }

        if (speed > speedLimit)
        {
            Vector2 speedVec = speedLimit * new Vector2(x, y);
            xSpeed = speedVec.x;
            ySpeed = speedVec.y;
        }

        //apply movement
        float deltaTime = Time.deltaTime;
        Vector3 delta = new Vector3(deltaTime * xSpeed, 0f, deltaTime * ySpeed);
        gameObject.transform.position += delta;
        
    }
}
