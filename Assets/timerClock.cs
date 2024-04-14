using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class timerClock : MonoBehaviour
{
    float currentTime;
    float initalStartTime;
    [SerializeField] TMP_Text timerText;
    public bool paused = false;

    public void Pause()
    {
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
        initalStartTime = Time.time + FindObjectOfType<LevelManager>().GetTimeForLevel();
    }

    string ConvertTimeToString(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        return minutes + ":" + seconds.ToString("00");
    }
    // Start is called before the first frame update
    void Start()
    {
        initalStartTime = FindObjectOfType<LevelManager>().GetTimeForLevel();
        timerText.text = ConvertTimeToString(initalStartTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(paused) { return; }
        currentTime = Mathf.Clamp(initalStartTime - Time.time, 0, 1000000);
        timerText.text = ConvertTimeToString(currentTime);
        if (currentTime == 0)
        {
            FindObjectOfType<LevelCompleteScreen>().ShowStatus();
        }
    }
}
