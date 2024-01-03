using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject menu;
    public Text winerName; 
    public static string currentName;
    bool menuIsOpend;
    public static bool isWin = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        menuIsOpend = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.isDead || isWin) && !menuIsOpend)
        {
            winer();
            menuIsOpend = true;
        } 
    }

    void winer()
    {
        Time.timeScale = 0.001f;
        if (isWin)
        {
            winerName.text = "You WIN";
            winerName.color = new Color(0, 1, 0);
        }
        else
        {
            winerName.text = "You LOSE";
            winerName.color = new Color(1, 0, 0);
        }
        menu.SetActive(true);
    }

    public void restartBtn()
    {
        WaitingMenu.isEnemyReady = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
