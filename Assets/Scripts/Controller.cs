using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //public GameObject[] platforms;

    public GameObject platformPrefab;
    public GameObject blobPrefab;
    public GameObject birdPrefab;
    public GameObject playerPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && FindObjectOfType<Player>() == null)
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(-7f, -2f, 0f), Quaternion.identity);
            player.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    public void SpawnNewEnemy()
    {
        int choice = Random.Range(0, 4);
        
        if (choice == 0)
        {
            GameObject enemy = Instantiate(birdPrefab, new Vector3(12f, 0f, 0f), Quaternion.identity);
            enemy.GetComponent<Enemy>().velocity = -5f;
        }
        else 
        {
            GameObject enemy = Instantiate(blobPrefab, new Vector3(12f, -2f, 0f), Quaternion.identity);
            enemy.GetComponent<Enemy>().velocity = -3f;
        }
    }

    public void SpawnNewPlatform()
    {
        GameObject platform = Instantiate(platformPrefab, new Vector3(14f, -5f, 0f), Quaternion.identity);
        platform.GetComponent<Platform>().velocity = -3f;
    }

}
