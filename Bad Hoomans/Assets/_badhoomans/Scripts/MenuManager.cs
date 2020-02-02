using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuManager : MonoBehaviour
{
    // menu scene objects 
    public Button startButton;
    public Button exitButton;
    public Text highScoreText;

    void StartBtnFunction()
    {
        SceneManager.LoadScene("main");
    }

    void ExitBtnFunction()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartBtnFunction);
        exitButton.onClick.AddListener(ExitBtnFunction);

        // TODO connect this function to end of game level
        // replace "10" with actual high score variable
        highScoreText.text = "10";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
