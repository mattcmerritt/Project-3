using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    private bool onGround;
    private Vector3 startPosition;

    private void Start()
    {
        onGround = true;
        startPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity += new Vector2(0f, 7f);
        }

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -8f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Floor")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }

}
