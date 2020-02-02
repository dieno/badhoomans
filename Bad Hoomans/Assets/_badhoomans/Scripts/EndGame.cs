using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    private float currentScore = 77; // TODO change this to create based on preferences
    private float highestScore = 99; // TODO change this to create based on preferences

    public Button retryBtn;
    public Button exitBtn;
    public Text currentScoreTxt;
    public Text highestScoreTxt;
    public Text scoreMessageTxt;

    void RetryBtnFunction()
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
        currentScoreTxt.text = currentScore.ToString();
        highestScoreTxt.text = highestScore.ToString();

        retryBtn.onClick.AddListener(RetryBtnFunction);
        exitBtn.onClick.AddListener(ExitBtnFunction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
