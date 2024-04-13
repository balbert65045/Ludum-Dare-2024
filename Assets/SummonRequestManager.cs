using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonRequestManager : MonoBehaviour
{
    [SerializeField] SummoningZone LinkedSummoningZone;
    [SerializeField] GameObject SummoningRequestPrefab;
    [SerializeField] TMP_Text ValueEarnedText;
    [SerializeField] float startDelay = 2f;
    float timeSinceLastSummon;
    SummonRequest myCurrentRequest;

    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSummon = Time.time;
        //RequestSummons();
    }

    private void Update()
    {
        if (!finished && myCurrentRequest == null && Time.time > timeSinceLastSummon + startDelay) {
            RequestSummons();
        }
    }

    public void RequestSummons()
    {
        RequestTypes[] requestTypes = FindObjectOfType<GameSummonManager>().AskForNextRequest();
        if (requestTypes.Length == 0)
        {
            finished = true;
            return;
        }
        GameObject request = Instantiate(SummoningRequestPrefab, this.transform);
        myCurrentRequest = request.GetComponent<SummonRequest>();
        myCurrentRequest.SetRequestType(requestTypes);
        request.transform.localPosition = Vector3.zero;
    }

    public void TriggerSummoning()
    {
        timeSinceLastSummon = Time.time;
        if (CheckIfRequestWorks(myCurrentRequest.myRequestTypes, LinkedSummoningZone.objsInCircle))
        {
            Debug.Log("Summon Works!");
            ValueEarnedText.gameObject.SetActive(true);
            ValueEarnedText.text = "+10";
            ValueEarnedText.color = Color.green;

            FindObjectOfType<Score>().AdjustScore(10);

        }
        else
        {
            ValueEarnedText.gameObject.SetActive(true);
            ValueEarnedText.text = "-10";
            ValueEarnedText.color = Color.red;

            FindObjectOfType<Score>().AdjustScore(-10);
            Debug.Log("Summon Fails");
        }
        LinkedSummoningZone.SummonObjs();
        StartCoroutine("WaitThenHideText");
    }

    IEnumerator WaitThenHideText()
    {
        yield return new WaitForSeconds(1);
        Destroy(myCurrentRequest.gameObject);
        ValueEarnedText.gameObject.SetActive(false);
    }


    bool CheckIfRequestWorks(List<RequestTypes> requestTypes, List<PickUpableObj> objects)
    {
        List<RequestTypes> remainingRequests = new List<RequestTypes>();
        List<PickUpableObj> remainingObjs = new List<PickUpableObj>();
        foreach(RequestTypes requestType in requestTypes) { remainingRequests.Add(requestType); }
        foreach(PickUpableObj pickUpableObj in objects) { remainingObjs.Add(pickUpableObj); }

        for(int i = 0; i < requestTypes.Count; i++)
        {
            for(int y = 0; y < objects.Count; y++) 
            {
                if (requestTypes[i] == objects[y].myRequestType)
                {
                    if (remainingRequests.Count > 0 && remainingObjs.Count > 0)
                    {
                        remainingRequests.Remove(requestTypes[i]);
                        remainingObjs.Remove(remainingObjs[y]);
                    }
                }
            }
        }

        return remainingRequests.Count == 0 && remainingObjs.Count == 0;
    }
}
