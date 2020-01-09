using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Path path;
    Rigidbody rigid;
    int actualPathIndex = 0;
    public float speed = 1.0f;
    bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.sleepThreshold = 0.0f;
        transform.position = path.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && !hasCollided)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 actualPoint = path.points[actualPathIndex];
        Vector3 nextPoint = path.points[actualPathIndex + 1];

        transform.LookAt(path.points[actualPathIndex + 1]);
        Vector3 diff = nextPoint - actualPoint;
        float length = diff.magnitude;
        transform.position += diff.normalized * speed * Time.deltaTime;
        if((transform.position - nextPoint).magnitude < 0.1f)
        {
            actualPathIndex++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer ==  LayerMask.NameToLayer("Obstacles"))
        {
            hasCollided = true;
            rigid.useGravity = true;
            rigid.AddForce((transform.position - collision.gameObject.transform.position) * 4.0f,ForceMode.Impulse);
        }
    }
    public int GetPathIndex()
    {
        return actualPathIndex;
    }
}
