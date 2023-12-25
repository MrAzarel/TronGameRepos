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

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        menuIsOpend = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isDead && !menuIsOpend)
        {
            winer();
            menuIsOpend = true;
        } 
    }

    void winer()
    {
        winerName.text = currentName + " Lose";
        menu.SetActive(true);
    }

    public void restartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
