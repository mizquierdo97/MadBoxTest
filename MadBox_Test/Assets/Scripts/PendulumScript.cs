using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumScript : MonoBehaviour
{
    public enum Axis
    {
        X_AXIS,
        Z_AXIS
    }

    //Used to change the movement direction
    public Axis movementAxis = Axis.X_AXIS;

    //Max movement distance & movement speed
    public float maxDist = 10.0f;
    public float speed = 10.0f;

    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        Vector3 moveVector = Vector3.right;
        switch(movementAxis)
        {
            case Axis.X_AXIS:
                {
                    moveVector = Vector3.right;
                }
                break;
            case Axis.Z_AXIS:
                {
                    moveVector = Vector3.forward;
                }
                break;
        }
        //Move in a sinusoidal function
        transform.position = initialPosition + moveVector * maxDist * Mathf.Sin(Time.time * speed);
    }
}
