using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float velocity;
    public Rigidbody2D rb;
    private Controller controller;

    private void Start()
    {
        controller = FindObjectOfType<Controller>();
    }

    private void Update()
    {
        transform.position += new Vector3(velocity * Time.deltaTime, 0f, 0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);

        // went off left of screen
        if (transform.position.x < -12f)
        {
            controller.SpawnNewEnemy();
            Destroy(gameObject);
        }

        // fell off bottom of screen
        if (transform.position.y < -3f)
        {
            controller.SpawnNewEnemy();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // do not allow enemies to stack
        if (collision.collider.tag == "Enemy")
        {
            controller.SpawnNewEnemy();
            Destroy(gameObject);
        }
    }

    // probably won't happen, but added just in case
    private void OnCollisionStay2D(Collision2D collision)
    {
        // do not allow enemies to stack
        if (collision.collider.tag == "Enemy")
        {
            controller.SpawnNewEnemy();
            Destroy(gameObject);
        }
    }
}
