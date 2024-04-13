using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RequestTypes
{
    Creature1,
}

public class SummonRequest : MonoBehaviour
{
    public List<RequestTypes> myRequestTypes;
    public float SummonTime = 6f;

    public void SetRequestType(RequestTypes[] types)
    {
        foreach(RequestTypes type in types)
        {
            myRequestTypes.Add(type);
        }
    }
}
