using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatingMenu : MonoBehaviour
{
    public UnityEngine.UI.Image enemyBackground;
    public Text ready;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.interactable = true;
        enemyBackground.color = new Color(0.5f, 0.5f, 0.5f);
        Time.timeScale = 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RedyBtn()
    {
        button.interactable = false;
        ready.text = "Ready";
        User.sendMessage("redy");
    }
}
