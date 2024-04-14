using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SummonRequestManager : MonoBehaviour
{
    [SerializeField] SummoningZone LinkedSummoningZone;
    [SerializeField] GameObject SummoningRequestPrefab;

    [SerializeField] TMP_Text ValueEarnedText;
    [SerializeField] float startDelay = 2f;
    float timeSinceLastSummon;
    SummonRequest myCurrentRequest;

    bool finished = false;
    [SerializeField] Image Icon;
    [SerializeField] GameObject Single;
    [SerializeField] GameObject Double;
    [SerializeField] GameObject Triple;
    [SerializeField] GameObject Four;

    [SerializeField] Image Arrow;

    public bool paused = false;
    public void Pause()
    {
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
        timeSinceLastSummon = Time.time + startDelay;
    }

    // Start is called before the first frame update
    void Start()
    {
        Arrow.gameObject.SetActive(false);
        timeSinceLastSummon = Time.time;
        Icon.sprite = LinkedSummoningZone.circle.sprite;
        Icon.color = LinkedSummoningZone.circle.color;
        LinkedSummoningZone.OnChangedInsidePortal = SumminingZone_OnChangedInsidePortal;
    }

    void SumminingZone_OnChangedInsidePortal()
    {
        Single.gameObject.SetActive(false);
        Double.gameObject.SetActive(false);
        Triple.gameObject.SetActive(false);
        Four.gameObject.SetActive(false);
        if (LinkedSummoningZone.objsInCircle.Count == 1)
        {
            Single.gameObject.SetActive(true);
            Image[] images = Single.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(LinkedSummoningZone.objsInCircle[i].myRequestType);
            }
        }
        else if (LinkedSummoningZone.objsInCircle.Count == 2)
        {
            Double.gameObject.SetActive(true);
            Image[] images = Double.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(LinkedSummoningZone.objsInCircle[i].myRequestType);
            }
        }
        else if (LinkedSummoningZone.objsInCircle.Count == 3)
        {
            Triple.gameObject.SetActive(true);
            Image[] images = Triple.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(LinkedSummoningZone.objsInCircle[i].myRequestType);
            }
        }
        else if (LinkedSummoningZone.objsInCircle.Count == 4)
        {
            Four.gameObject.SetActive(true);
            Image[] images = Four.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(LinkedSummoningZone.objsInCircle[i].myRequestType);
            }
        }
        if (CheckIfRequestWorks(myCurrentRequest.myRequestTypes, LinkedSummoningZone.objsInCircle))
        {
            Arrow.color = Color.green;
        }
        else
        {
            Arrow.color = Color.red;
        }
    }

    private void Update()
    {
        if (paused) { return; }
        if (!finished && myCurrentRequest == null && Time.time > timeSinceLastSummon + startDelay) {
            RequestSummons();
        }
    }

    

    public void RequestSummons()
    {
        Arrow.gameObject.SetActive(true);
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
        if (CheckIfRequestWorks(myCurrentRequest.myRequestTypes, LinkedSummoningZone.objsInCircle))
        {
            Arrow.color = Color.green;
        }
        else
        {
            Arrow.color = Color.red;
        }
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
            LinkedSummoningZone.SuccessAudio.Play();
            FindObjectOfType<Score>().AdjustScore(10);

        }
        else
        {
            ValueEarnedText.gameObject.SetActive(true);
            ValueEarnedText.text = "-10";
            ValueEarnedText.color = Color.red;
            LinkedSummoningZone.FailAudio.Play();
            FindObjectOfType<Score>().AdjustScore(-10);
            Debug.Log("Summon Fails");
        }
        LinkedSummoningZone.SummonObjs();
        Destroy(myCurrentRequest.gameObject);
        Arrow.gameObject.SetActive(false);
        StartCoroutine("WaitThenHideText");
    }

    IEnumerator WaitThenHideText()
    {
        yield return new WaitForSeconds(1);
        ValueEarnedText.gameObject.SetActive(false);
    }


    bool CheckIfRequestWorks(List<RequestTypes> requestTypes, List<PickUpableObj> objects)
    {
        List<RequestTypes> remainingRequests = new List<RequestTypes>();
        List<RequestTypes> remainingObjsRequests = new List<RequestTypes>();
        foreach(RequestTypes requestType in requestTypes) { remainingRequests.Add(requestType); }
        foreach(PickUpableObj pickUpableObj in objects) { remainingObjsRequests.Add(pickUpableObj.myRequestType); }

        for(int i = 0; i < requestTypes.Count; i++)
        {
            if (remainingObjsRequests.Contains(requestTypes[i])){ 
                remainingObjsRequests.Remove(requestTypes[i]);
                remainingRequests.Remove(requestTypes[i]);
            }
            /*
            for(int y = 0; y < objects.Count; y++) 
            {
                if (requestTypes[i] == objects[y].myRequestType)
                {
                    if (remainingRequests.Count > i && remainingObjs.Count > y)
                    {
                        remainingRequests.Remove(requestTypes[i]);
                        remainingObjs.Remove(remainingObjs[y]);
                    }
                }
            }
            */
        }

        return remainingRequests.Count == 0 && remainingObjsRequests.Count == 0;
    }
}
