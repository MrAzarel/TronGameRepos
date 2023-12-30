using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static string allData;
    string direction;

    public float speed = 16.0f;
    public GameObject wallPrefab;
    Vector2 lastWallEnd;
    Collider2D wall;
    public static bool isDead;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WatingMenu.isGameStarted)
        {
            gameStart();

            getDirection();

            move();

            fitColliderBetween(wall, lastWallEnd, transform.position);
        }
    }

    void getDirection()
    {
        direction = allData.Split(' ')[0];
    }

    void move()
    {
        if (direction == "w")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnWall();
        }
        else if (direction == "s")
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            spawnWall();
        }
        else if (direction == "d")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
        }
        else if (direction == "a")
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            spawnWall();
        }
    }

    void spawnWall()
    {
        lastWallEnd = transform.position;

        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void fitColliderBetween(Collider2D collider, Vector2 vectorA, Vector2 vectorB)
    {
        collider.transform.position = vectorA + (vectorB - vectorA) * 0.5f;

        float dist = Vector2.Distance(vectorA, vectorB);
        if (vectorA.x != vectorB.x)
            collider.transform.localScale = new Vector2(dist + 1, 1);
        else
            collider.transform.localScale = new Vector2(1, dist + 1);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != wall)
        {
            isDead = true;
            endGame();
        }
    }

    void endGame()
    {
        Destroy(gameObject);
        Time.timeScale = 0.001f;
        Restart.currentName = name;
    }

    void gameStart()
    {
        if (WatingMenu.isGameStarted)
        {
            isDead = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnWall();
            WatingMenu.isGameStarted = false;
        }
    }
}
