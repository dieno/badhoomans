using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    // menu scene objects 
    public Button startButton;
    public Button exitButton;
    public Text highScoreText;

    // game scene objects
    public Text currentScoreText;
    public Image hpImage;

    // private variables
    private float currentScore = 0.0f;
    private double time = -1;
    private const float hitPoint = 0.5f;
    private const float maxHp = 10.0f;
    private float currentHp = maxHp;


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

    void takeHit()
    {
        if (currentHp > 0)
        {
            currentHp -= hitPoint;
            hpImage.fillAmount -= (hitPoint/maxHp);
        }
    }

    void heal()
    {
        if(currentHp < maxHp)
        {
            currentHp += hitPoint;
            hpImage.fillAmount += (hitPoint / maxHp);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //startButton.onClick.AddListener(UpdateHighScore);
        startButton.onClick.AddListener(StartBtnFunction);
        exitButton.onClick.AddListener(ExitBtnFunction);
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now.Millisecond != time)
        {
            time = DateTime.Now.Millisecond;
            // fine-tune the timer speed by changing this number
            currentScore += 0.05f;
            currentScoreText.text = Mathf.Ceil(currentScore).ToString();
        }

        // TODO this is only for testing to simulate a hit
        if (Input.GetKeyDown("z"))
        {
            takeHit();
        }
        if(Input.GetKeyDown("x"))
        {
            heal();
        }

    }
}
