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
        SceneManager.LoadScene("test");
    }

    void ExitBtnFunction()
    {
        Application.Quit();
    }

    void UpdateHighScore()
    {
        // connect this function to end of game level
        // replace "10" with actual high score variable
        highScoreText.text = "10";
    }

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartBtnFunction);
        exitButton.onClick.AddListener(ExitBtnFunction);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
