using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] Sprite StarOn;
    [SerializeField] Sprite StarOff;
    [SerializeField] GameObject LevelCompletePanel;
    [SerializeField] Image Star1;
    [SerializeField] TMP_Text Star1ScoreReq;

    [SerializeField] Image Star2;
    [SerializeField] TMP_Text Star2ScoreReq;

    [SerializeField] Image Star3;
    [SerializeField] TMP_Text Star3ScoreReq;

    [SerializeField] TMP_Text ScoreText;

    LevelManager levelManager;

    private void Start()
    {
        Time.timeScale = 1f;
        levelManager = FindObjectOfType<LevelManager>();
    }
    public void ShowStatus()
    {
        Time.timeScale = 0f;
        LevelCompletePanel.SetActive(true);
        int score = FindObjectOfType<Score>().currentScore;
        ScoreText.text = score.ToString();
        int firstStarScore = levelManager.GetScoreForFirstStar();
        Star1ScoreReq.text = firstStarScore.ToString();
        int secondStarScore = levelManager.GetScoreForSecondStar();
        Star2ScoreReq.text = secondStarScore.ToString();
        int thirdStarScore = levelManager.GetScoreForThirdStar();
        Star3ScoreReq.text = thirdStarScore.ToString();

        if (score >= firstStarScore)
        {
            Star1.sprite = StarOn;
        }
        else
        {
            Star1.sprite = StarOff;
        }

        if (score >= secondStarScore)
        {
            Star2.sprite = StarOn;
        }
        else
        {
            Star2.sprite = StarOff;
        }

        if (score >= thirdStarScore)
        {
            Star3.sprite = StarOn;
        }
        else
        {
            Star3.sprite = StarOff;
        }

    }
}
