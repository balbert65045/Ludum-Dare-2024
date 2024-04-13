using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField] TMP_Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = currentScore.ToString();
    }

    public void AdjustScore(int value)
    {
        currentScore += value;
        ScoreText.text = currentScore.ToString();
    }
}
