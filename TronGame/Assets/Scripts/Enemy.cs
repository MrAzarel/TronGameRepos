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

    bool isStarted;

    string lastPressedButton = "w";

    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaitingMenu.isGameStarted)
        {
            if (!isStarted)
            {
                gameStart();
            }

            direction = getDirection();

            move();

            fitColliderBetween(wall, lastWallEnd, transform.position);
        }
    }

    string getDirection()
    {
       return allData.Split(' ')[0];
    }

    void move()
    {
        transform.position = new Vector3(float.Parse(allData.Split(' ')[1]), float.Parse(allData.Split(' ')[2]));
        if (allData.Split(' ')[0] != lastPressedButton)
        {
            spawnWall();
            lastPressedButton = allData.Split(' ')[0];
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
        isDead = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
        isStarted = true;
    }
}
