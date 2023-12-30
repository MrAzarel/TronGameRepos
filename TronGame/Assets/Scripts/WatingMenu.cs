using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatingMenu : MonoBehaviour
{
    public GameObject watingMenu;

    public UnityEngine.UI.Image enemyBackground;
    public UnityEngine.UI.Image playerBackground;
    public Text ready;
    public Button readyButton;
    public static bool isEnemyReady = false;
    public static string startSide = "right";

    public GameObject youText;
    public GameObject enemyText;
    public GameObject enemyReady;

    public GameObject player;
    public GameObject enemy;

    bool isPlayerReady = false;

    void Start()
    {
        watingMenu.SetActive(true);
        ChooseSide(startSide);
        readyButton.interactable = true;
        enemyBackground.color = new Color(0.5f, 0.5f, 0.5f);
        playerBackground.color = new Color(0.5f, 0.5f, 0.5f);
        Time.timeScale = 0.001f;
    }

    void Update()
    {
        MakeEnemyReady();
        StartGame();
    }

    public void RedyBtn()
    {
        readyButton.interactable = false;
        ready.text = "Ready";
        if (startSide == "right")
            enemyBackground.color = new Color(1, 1, 1);
        else
            playerBackground.color = new Color(1, 1, 1);
        User.sendMessage("redy");
    }

    void ChooseSide(string side)
    {
        if (side == "right")
        {
            youText.transform.position = new Vector3(872, 511);
            enemyText.transform.position = new Vector3(270, 511);
            readyButton.transform.position = new Vector3(872, 180);
            enemyReady.transform.position = new Vector3(270, 180);
            player.transform.position = new Vector3(3, 0);
            enemy.transform.position= new Vector3(-3, 0);
        }
    }

    void MakeEnemyReady()
    {
        if (isEnemyReady) 
        {
            if (startSide == "right")
                playerBackground.color = new Color(1, 1, 1);
            else
                enemyBackground.color = new Color(1, 1, 1);
            enemyReady.GetComponent<Text>().text = "ready";
        }
    }

    void StartGame()
    {
        if (isEnemyReady && isPlayerReady)
        {
            watingMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
