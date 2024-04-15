using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text ScoreMultiplier;
    public int multiplierValue = 1;

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

    public void ChangeMultiplier(bool up)
    {
        if (!up) { multiplierValue = 1; }
        else { multiplierValue = Mathf.Clamp(multiplierValue + 1, 0, 5); }
        if (multiplierValue == 1) { ScoreMultiplier.text = ""; }
        else { ScoreMultiplier.text = "x"+multiplierValue.ToString(); }
    }
}
