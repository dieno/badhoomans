using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    // game systems
    [SerializeField] private ProjectileSystem projectileSystem = null;

    // game scene objects
    public Text currentScoreText;
    public Image hpImage;
    public Image meteorIndicator;

    // private variables
    private float currentScore = 0.0f;
    private double time = -1;
    private const float hitPoint = 2f;
    private const float maxHp = 10.0f;
    private float currentHp = maxHp;

    public void takeHit()
    {
        if (currentHp > 0)
        {
            currentHp -= hitPoint;
            hpImage.fillAmount -= (hitPoint/maxHp);
        }
    }

    public void heal()
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
        projectileSystem.Begin();
        Cursor.visible = false;
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




        if(currentHp <= 0f)
        {
            float currentHighscore = PlayerPrefs.GetFloat("HIGHSCORE", 0);

            if(currentScore > currentHighscore)
            {
                PlayerPrefs.SetFloat("HIGHSCORE", currentScore);
            }

            PlayerPrefs.SetFloat("SCORE", currentScore);

            SceneManager.LoadScene("end");
        }


        //// TODO this is only for testing to simulate a hit
        //if (Input.GetKeyDown("z"))
        //{
        //    takeHit();
        //}
        //if(Input.GetKeyDown("x"))
        //{
        //    heal();
        //}

    }
}
