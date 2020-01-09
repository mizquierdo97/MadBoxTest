using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    //Path to follow
    public Path path;
    int actualPathIndex = 0;

    //COMPONENTS--
    Rigidbody rigid;
    //------------

    //Movement speed
    public float speed = 1.0f;

    //Respawn variables
    bool hasCollided = false;
    float respawnTimer = 0.0f;
    float respawnTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.sleepThreshold = 0.0f;

        //Start at path position 0
        transform.position = path.points[0];

        //Add listener to reset level
        ScoreManager.Instance.OnResetLevel.AddListener(ResetLevel);
    }

    // Update is called once per frame
    void Update()
    {
        //If game is not active ignore input
        if (!ScoreManager.Instance.isGameActive) return;

        //Usefull for camera position
        transform.LookAt(path.points[actualPathIndex + 1]);

        //Input
        if (Input.GetKey(KeyCode.Space) && !hasCollided)
        {
            Move();
        }

        //Check if it collide with some obstacle to restart the level
        if(hasCollided)
        {
            respawnTimer += Time.deltaTime;
            if(respawnTimer >= respawnTime)
            {
                ScoreManager.Instance.RemoveLive();
                Respawn();
            }
        }
    }

    private void Move()
    {
        //Next point of the path
        Vector3 nextPoint = path.points[actualPathIndex + 1];
        
        //Calculate Direction
        Vector3 diff = nextPoint - transform.position;
        transform.position += diff.normalized * speed * Time.deltaTime;

        //Check if has arrived to the next path position
        if ((transform.position - nextPoint).magnitude < 0.1f)
        {
            actualPathIndex++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //OBSTACLES
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            hasCollided = true;
            rigid.useGravity = true;
            rigid.AddForce((transform.position - collision.gameObject.transform.position) * 4.0f, ForceMode.Impulse);
        }
        //---------

        //GROUND COLLIDER
        if (collision.gameObject.layer == LayerMask.NameToLayer("LoseCollider"))
        {
            ScoreManager.Instance.RemoveLive();
            Respawn();
        }
        //--------------

        //WIN COLLIDER
        if (collision.gameObject.layer == LayerMask.NameToLayer("WinCollider"))
        {
            ScoreManager.Instance.ShowScore();
        }
        //---------
    }
    private void Respawn()
    {
        //Reset some of the variables
        respawnTimer = 0.0f;
        hasCollided = false;
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        transform.position = path.points[actualPathIndex];
    }

    private void ResetLevel()
    {
        actualPathIndex = 0;
         Respawn();
    }
    public int GetPathIndex()
    {
        return actualPathIndex;
    }
}
