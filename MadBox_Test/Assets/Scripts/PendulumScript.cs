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
    public Axis movementAxis = Axis.X_AXIS;
    public float maxDist = 10.0f;
    public float speed = 10.0f;

    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
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
        transform.position = initialPosition + moveVector * maxDist * Mathf.Sin(Time.time * speed);
    }
}
