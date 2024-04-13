using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum RequestTypes
{
    Creature1,
    Creature2,
    Creature3,
    Creature4
}

public class SummonRequest : MonoBehaviour
{
    [SerializeField] Image Circle;
    [SerializeField] Image RequestObj;
    public List<RequestTypes> myRequestTypes;
    public float SummonTime = 6f;

    public void SetRequestType(RequestTypes[] types, SpriteRenderer circle)
    {
        Circle.color = circle.color;
        Circle.sprite = circle.sprite;
        foreach (RequestTypes type in types)
        {
            myRequestTypes.Add(type);
        }
        if (myRequestTypes.Count == 1)
        {
            RequestObj.sprite = FindObjectOfType<SummonLibrary>().findSprite(myRequestTypes[0]);
        }
    }
}
