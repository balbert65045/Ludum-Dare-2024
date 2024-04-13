using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Request
{
    public RequestTypes[] types;
}

public class GameSummonManager : MonoBehaviour
{
    [SerializeField] SummonRequestManager[] requestManagers;
    public List<Request> requestsForTheLevel = new List<Request>();

    public List<GameObject> SpawnLocations = new List<GameObject>();

    public RequestTypes[] AskForNextRequest()
    {
        if (requestsForTheLevel.Count == 0)
        {
            return new RequestTypes[0];
        }
        else
        {
            Request request = requestsForTheLevel[0];
            requestsForTheLevel.Remove(request);
            List<GameObject> prefabs = GetComponent<SummonLibrary>().GetObjsFromTypeOfSummon(request.types);
            foreach (GameObject obj in prefabs)
            {
                GameObject SpawnLoc = SpawnLocations[UnityEngine.Random.Range(0, SpawnLocations.Count)];
                Instantiate(obj, SpawnLoc.transform.position, Quaternion.identity);
            }
            return request.types;
        }
    }
}
