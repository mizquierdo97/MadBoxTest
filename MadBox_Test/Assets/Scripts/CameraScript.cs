using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerMovement player;
    public Path path;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - path.points[player.GetPathIndex() + 1];        
        direction.y = 0.0f;        
        direction.Normalize(); Debug.Log(direction);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + player.transform.right * 10.0f  + player.transform.up * 10.0f + direction * 10.0f, 0.05f);
        transform.LookAt(player.transform);
    }
}
