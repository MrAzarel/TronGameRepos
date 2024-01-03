using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    public float speed = 16.0f;

    public GameObject wallPrefab;

    Vector2 lastWallEnd;

    Collider2D wall;

    public static bool isDead;

    string lastPressedButton = "w";

    bool isStarted;

    string x;
    string y;

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

            move();

            fitColliderBetween(wall, lastWallEnd, transform.position);

            User.sendMessage(collectMessage());
        }
    }

    void move()
    {
        if (Input.GetKeyDown(upKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnWall();
            lastPressedButton = "w";
        }
        else if (Input.GetKeyDown(downKey))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            spawnWall();
            lastPressedButton = "s";
        }
        else if (Input.GetKeyDown(rightKey))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
            lastPressedButton = "d";
        }
        else if (Input.GetKeyDown(leftKey))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            spawnWall();
            lastPressedButton = "a";
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
        User.sendMessage("dead");
        Destroy(gameObject);
        Restart.currentName = name;
    }

    string collectMessage()
    {
        x =  transform.position.x.ToString();
        y = transform.position.y.ToString();
        return lastPressedButton + " " + x + " " + y + " true";
    }

    void gameStart()
    {
        isDead = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
        isStarted = true;
    }
}
