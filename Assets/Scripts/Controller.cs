using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{

    public GameObject platformPrefab;
    public GameObject blobPrefab;
    public GameObject birdPrefab;
    public GameObject playerPrefab;

    public float minBirdTimer;
    public float minBlobTimer;

    public bool birdQueued;
    public bool blobQueued;

    public static float score;
    public static float maxScore;
    public TMP_Text scoreLabel;
    public TMP_Text highScoreLabel;

    private void Start()
    {
        minBirdTimer = 1.5f;
        minBlobTimer = 2;

        birdQueued = false;
        blobQueued = false;

        score = 0;
        scoreLabel.text = "Score: " + Mathf.Round(score);

        maxScore = 0;
        highScoreLabel.text = "High Score: " + Mathf.Round(maxScore);
    }

    private void Update()
    {
        score += Time.deltaTime;
        minBirdTimer -= Time.deltaTime;
        minBlobTimer -= Time.deltaTime;

        if (FindObjectOfType<Player>() == null)
        {
            maxScore = Mathf.Max(score, maxScore);
            score = 0;
        }

        // if an enemy gets lost, add another
        if (FindObjectsOfType<Enemy>().Length < 3)
        {
            if (!blobQueued && birdQueued)
            {
                Debug.Log("Queued missing blob");
                blobQueued = true;
            }
            if (!birdQueued && blobQueued)
            {
                Debug.Log("Queued missing bird");
                birdQueued = true;
            }
        }

        // if too many enemies are added, take the one away that is farthest to the right
        if (FindObjectsOfType<Enemy>().Length > 3)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Enemy farthestRight = enemies[0];
            for (int i = 1; i < enemies.Length; i++)
            {
                if (farthestRight.transform.position.x < enemies[i].transform.position.x)
                {
                    farthestRight = enemies[i];
                }
            }
            Destroy(farthestRight.gameObject);
        }

        scoreLabel.text = "Current Score: " + Mathf.Round(score);
        highScoreLabel.text = "High Score: " + Mathf.Round(Mathf.Max(maxScore, score));

        if (Input.GetKeyDown(KeyCode.Space) && FindObjectOfType<Player>() == null)
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(-7f, -2f, 0f), Quaternion.identity);
            player.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (birdQueued && minBirdTimer <= 0f)
        {
            // reset timer and queue
            minBirdTimer = 1.5f;
            birdQueued = false;
            // spawn enemy
            GameObject enemy = Instantiate(birdPrefab, new Vector3(12f, 0f, 0f), Quaternion.identity);
            enemy.GetComponent<Enemy>().velocity = -5f;
        }

        if (blobQueued && minBlobTimer <= 0f)
        {
            // reset time and queue
            minBlobTimer = 2f;
            blobQueued = false;
            // spawn enemy
            GameObject enemy = Instantiate(blobPrefab, new Vector3(12f, -2f, 0f), Quaternion.identity);
            enemy.GetComponent<Enemy>().velocity = -3f;
        }
    }

    public void SpawnNewEnemy()
    {
        int choice = Random.Range(0, 5);
        
        if (choice == 0) 
        {
            if (minBirdTimer <= 0f) // timer has expired
            {
                // reset timer and queue
                minBirdTimer = 1.5f;
                birdQueued = false;
                // spawn enemy
                GameObject enemy = Instantiate(birdPrefab, new Vector3(12f, 0f, 0f), Quaternion.identity);
                enemy.GetComponent<Enemy>().velocity = -5f;
            }
            else
            {
                birdQueued = true;
                Debug.Log("Queued a bird.");
            }
        }
        else 
        {
            if (minBlobTimer <= 0f)
            {
                // reset time and queue
                minBlobTimer = 2f;
                blobQueued = false;
                // spawn enemy
                GameObject enemy = Instantiate(blobPrefab, new Vector3(12f, -2f, 0f), Quaternion.identity);
                enemy.GetComponent<Enemy>().velocity = -3f;
            }
            else
            {
                blobQueued = true;
                Debug.Log("Queued a blob.");
            }
            
        }
    }

    public void SpawnNewPlatform()
    {
        GameObject platform = Instantiate(platformPrefab, new Vector3(14f, -5f, 0f), Quaternion.identity);
        platform.GetComponent<Platform>().velocity = -3f;
    }

    public void ShortenBlobDelay()
    {
        minBlobTimer = 0.25f;
    }

}
