using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    public Path path;
    Rigidbody rigid;
    int actualPathIndex = 0;
    public float speed = 1.0f;
    bool hasCollided = false;
    float respawnTimer = 0.0f;
    float respawnTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.sleepThreshold = 0.0f;
        transform.position = path.points[0];
        ScoreManager.Instance.OnResetLevel.AddListener(ResetLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ScoreManager.Instance.isGameActive) return;

        transform.LookAt(path.points[actualPathIndex + 1]);
        if (Input.GetKey(KeyCode.Space) && !hasCollided)
        {
            Move();
        }
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
        Vector3 nextPoint = path.points[actualPathIndex + 1];
        
        Vector3 diff = nextPoint - transform.position;
        transform.position += diff.normalized * speed * Time.deltaTime;
        if ((transform.position - nextPoint).magnitude < 0.1f)
        {
            actualPathIndex++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            hasCollided = true;
            rigid.useGravity = true;
            rigid.AddForce((transform.position - collision.gameObject.transform.position) * 4.0f, ForceMode.Impulse);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("LoseCollider"))
        {
            ScoreManager.Instance.RemoveLive();
            Respawn();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("WinCollider"))
        {
            ScoreManager.Instance.ShowScore();
        }
    }
    private void Respawn()
    {
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
