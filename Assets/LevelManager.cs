using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float timeForLevel = 60f;
    public float GetTimeForLevel() { return timeForLevel; }
    [SerializeField] int scoreForFirstStar = 20;
    public int GetScoreForFirstStar() { return scoreForFirstStar; }

    [SerializeField] int scoreForSecondStar = 40;
    public int GetScoreForSecondStar() { return scoreForSecondStar; }

    [SerializeField] int scoreForThirdStar = 60;
    public int GetScoreForThirdStar() { return scoreForThirdStar; }


}
