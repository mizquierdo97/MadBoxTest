using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumScript : MonoBehaviour
{
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
        transform.position = initialPosition + Vector3.right * maxDist * Mathf.Sin(Time.time * speed);
    }
}
