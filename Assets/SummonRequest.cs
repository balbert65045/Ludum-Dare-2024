using System;
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
    [SerializeField] GameObject Single;
    [SerializeField] GameObject Double;
    [SerializeField] GameObject Triple;
    [SerializeField] GameObject Four;

    public List<RequestTypes> myRequestTypes;
    public float SummonTime = 6f;

    public void SetRequestType(RequestTypes[] types)
    {
        foreach (RequestTypes type in types)
        {
            myRequestTypes.Add(type);
        }
        if (myRequestTypes.Count == 1)
        {
            Single.gameObject.SetActive(true);
            Image[] images = Single.GetComponentsInChildren<Image>();
            for(int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(myRequestTypes[i]);
            }
        }
        else if (myRequestTypes.Count == 2)
        {
            SummonTime = SummonTime * 1.5f;

            Double.gameObject.SetActive(true);
            Image[] images = Double.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(myRequestTypes[i]);
            }
        }
        else if (myRequestTypes.Count == 3)
        {
            SummonTime = SummonTime * 2f;

            Triple.gameObject.SetActive(true);
            Image[] images = Triple.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(myRequestTypes[i]);
            }
        }
        else if (myRequestTypes.Count == 4)
        {
            SummonTime = SummonTime * 3f;

            Four.gameObject.SetActive(true);
            Image[] images = Four.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = FindObjectOfType<SummonLibrary>().findSprite(myRequestTypes[i]);
            }
        }
        SummonTime = SummonTime * FindObjectOfType<LevelManager>().levelAdjustmentTimeFactor;
    }
}
