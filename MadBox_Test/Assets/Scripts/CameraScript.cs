using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public PlayerMovement player;
    public Path path;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Transform playerTrans = player.transform;

        //Calculate Player Direction
        Vector3 direction = playerTrans.position - path.points[player.GetPathIndex() + 1];        
        direction.y = 0.0f;        
        direction.Normalize();

        //Calculate camera position
        Vector3 camDesiredPos = playerTrans.position + playerTrans.right * 10.0f + playerTrans.up * 10.0f + direction * 10.0f;

        //LERP to make things smoother
        transform.position = Vector3.Lerp(transform.position, camDesiredPos, 0.05f);

        //Always looking at the player
        transform.LookAt(playerTrans);
    }
}
