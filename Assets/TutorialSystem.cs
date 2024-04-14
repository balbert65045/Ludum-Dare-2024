using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialEvent
{
    Nothing,
    Spawn,
    ResumeSummonTimer,
    TriggerMultipleSpawn
}

public class TutorialSystem : MonoBehaviour
{
    RandomSummonCreations randomCreations;
    SummonRequestManager[] summonManagers;
    timerClock clock;

    [SerializeField] TutorialPiece[] tutorialpieces;
    int currentTutorialIndex = 0;

    bool pausePoint = false;

    // Start is called before the first frame update
    void Start()
    {
        randomCreations = FindObjectOfType<RandomSummonCreations>();
        clock = FindObjectOfType<timerClock>();
        summonManagers = FindObjectsOfType<SummonRequestManager>();

        randomCreations.Pause();
        clock.Pause();
        foreach(SummonRequestManager manager in summonManagers) { manager.Pause(); }
        tutorialpieces[currentTutorialIndex].gameObject.SetActive(true);
    }

    void UnPause()
    {
        randomCreations.UnPause();
        clock.UnPause();
        FindObjectOfType<SummonTimer>().ResumeTimer();
        foreach (SummonRequestManager manager in summonManagers) { manager.UnPause(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorialpieces[currentTutorialIndex].gameObject.SetActive(false);
            if (pausePoint) { return; }
            currentTutorialIndex++;
            if(currentTutorialIndex >= tutorialpieces.Length) { UnPause(); }
            else
            {
                tutorialpieces[currentTutorialIndex].gameObject.SetActive(true);
                CheckEvent(tutorialpieces[currentTutorialIndex].tutorialEvent);
            }
        }
    }

    void CheckEvent(TutorialEvent tutorialEvent)
    {
        if (tutorialEvent == TutorialEvent.Spawn)
        {
            summonManagers[0].RequestSummons();
            FindObjectOfType<SummonTimer>().PauseTimer();
            FindObjectOfType<SummonTimer>().OnSummonTriggered += SummonTimerFinished;
        }
        else if (tutorialEvent == TutorialEvent.ResumeSummonTimer)
        {
            FindObjectOfType<SummonTimer>().ResumeTimer();
            pausePoint = true;
        }
        else if(tutorialEvent == TutorialEvent.TriggerMultipleSpawn)
        {
            summonManagers[0].RequestSummons();
            FindObjectOfType<SummonTimer>().PauseTimer();
            randomCreations.SummonRandomCreature();
        }
    }

    void SummonTimerFinished()
    {
        StartCoroutine("WaitAndThenFinish");
    }

    IEnumerator WaitAndThenFinish()
    {
        yield return new WaitForSeconds(.3f);
        pausePoint = false;
        if (tutorialpieces[currentTutorialIndex + 1].tutorialEvent == TutorialEvent.TriggerMultipleSpawn)
        {
            tutorialpieces[currentTutorialIndex].gameObject.SetActive(false);
            currentTutorialIndex++;
            tutorialpieces[currentTutorialIndex].gameObject.SetActive(true);
            CheckEvent(tutorialpieces[currentTutorialIndex].tutorialEvent);
        }
    }
}
