using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rbody2D;
    public float speed = 9;
    public Vector3 vel;
    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isPlaying == false)
        {
            ResetAndSendBallInRandomDirection();
        }
    }

    private void ResetBall()
    {
        rbody2D.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        isPlaying = false;
    }

    private void ResetAndSendBallInRandomDirection()
    {
        ResetBall();
        rbody2D.velocity = GenerateRandomVelocity(true) * speed;
        vel = rbody2D.velocity;
        isPlaying = true;
    }

    private Vector3 GenerateRandomVelocity(bool shouldReturnNormalized)
    {
        Vector3 velocity = new Vector3();
        bool shouldGoRight = Random.Range(1, 100) > 50;
        bool shouldGoLeft = Random.Range(1, 100) < 50;
        velocity.x = shouldGoRight ? Random.Range(-.8f, -.2f): Random.Range(.8f, .2f);
        velocity.y = shouldGoLeft ? Random.Range(-.8f, .2f): Random.Range(.8f, .2f);

        if (shouldReturnNormalized)
        {
            return velocity.normalized;
        }
        return velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 newVelocity = vel;
        newVelocity += new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        rbody2D.velocity = Vector3.Reflect(newVelocity.normalized * speed, collision.contacts[0].normal);
        vel = rbody2D.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
            print("Lewy +1");
        if (transform.position.x < 0)
            print("Prawy +1");
        ResetBall();
    }
}
