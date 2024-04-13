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
        initialSummoningTime = Time.time;
    }

    public void TriggerSummoning()
    {
        //Trigger score and stuff
        GetComponentInParent<SummonRequestManager>().TriggerSummoning();
    }

    // Update is called once per frame
    void Update()
    {
        if (summoning)
        {
            float timeDifference = Time.time - initialSummoningTime;
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
