
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class WaitingMenu : MonoBehaviour
{
    public GameObject watingMenu;

    public UnityEngine.UI.Image enemyBackground;
    public UnityEngine.UI.Image playerBackground;
    public Text ready;
    public Button readyButton;
    public static string startSide;

    public GameObject youText;
    public GameObject enemyText;
    public GameObject enemyReady;

    public GameObject player;
    public GameObject enemy;

    public static bool isPlayerReady = false;
    public static bool isEnemyReady = false;

    public static bool isGameStarted = false;
    public static string dataToSend;

    public static int readyCount = 0;

    void Start()
    {
        Time.timeScale = 0.001f;
        watingMenu.SetActive(true);
        ChooseSide(startSide);
        readyButton.interactable = true;
        enemyBackground.color = new Color(0.5f, 0.5f, 0.5f);
        playerBackground.color = new Color(0.5f, 0.5f, 0.5f);
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
        isPlayerReady = true;
        if (startSide == "right")
            enemyBackground.color = new Color(1, 1, 1);
        else
            playerBackground.color = new Color(1, 1, 1);
        User.sendMessage("ready");
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
        if (isPlayerReady && isEnemyReady)
        {
            watingMenu.SetActive(false);          
            Time.timeScale = 1f;
            isGameStarted = true;
        }
    }

    public void backBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
