using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuButtons;
    public GameObject enter;
    public Text ip;
    public string ipString;
    User user = new User();

    // Start is called before the first frame update
    void Start()
    {
        menuButtons.SetActive(true);
        enter.SetActive(false);
    }

    private void Update()
    {
        if (User.isConnected)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void EscapeBtn()
    {
        Application.Quit();
    }

    public void PlayBtn()
    {
        menuButtons.SetActive(false);
        enter.SetActive(true);
    }

    public void BackBtn()
    {
        menuButtons.SetActive(true);
        enter.SetActive(false);
    }

    public void ConnectBtn()
    {
        ipString = ip.text.ToString();
        user.Connect(ipString);
    }
}