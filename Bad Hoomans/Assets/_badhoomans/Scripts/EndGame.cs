using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    private float currentScore = 0;
    private float highestScore = 0;

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
        currentScore = PlayerPrefs.GetFloat("SCORE", 0);
        highestScore = PlayerPrefs.GetFloat("HIGHSCORE", 0);

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
