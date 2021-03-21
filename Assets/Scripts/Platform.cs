using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float velocity;
    private Controller controller;

    private void Start()
    {
        controller = FindObjectOfType<Controller>();
    }

    private void Update()
    {
        transform.position += new Vector3(velocity * Time.deltaTime, 0f, 0f);

        // went off left of screen
        if (transform.position.x < -14f)
        {
            controller.SpawnNewPlatform();
            Destroy(gameObject);
        }
    }

}
