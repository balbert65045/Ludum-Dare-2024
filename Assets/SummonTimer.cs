using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonTimer : MonoBehaviour
{
    bool summoning = false;
    float initialSummoningTime;
    [SerializeField] Slider slider;

    float SummonTime;

    bool paused = false;

    public Action OnSummonTriggered;

    public void PauseTimer()
    {
        paused = true;
    }

    public void ResumeTimer()
    {
        paused = false;
        initialSummoningTime = Time.timeSinceLevelLoad;
    }
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        SummonTime = GetComponentInParent<SummonRequest>().SummonTime;
        StartSummoning();
    }

    public void StartSummoning()
    {
        summoning = true;
        initialSummoningTime = Time.timeSinceLevelLoad;
    }

    public void TriggerSummoning()
    {
        if (OnSummonTriggered != null) OnSummonTriggered();
        //Trigger score and stuff
        GetComponentInParent<SummonRequestManager>().TriggerSummoning();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) { return; }
        if (summoning)
        {
            float timeDifference = Time.timeSinceLevelLoad - initialSummoningTime;
            float percentage = Mathf.Clamp(timeDifference, 0.0f, SummonTime)/ SummonTime;
            slider.value = percentage;
            if (percentage >= 1)
            {
                summoning = false;
                TriggerSummoning();
            }
        }
    }
}
