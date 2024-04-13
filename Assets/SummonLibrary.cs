using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonLibrary : MonoBehaviour
{
    public Sprite Creatur1Sprite;
    public GameObject Creatur1Prefab;
    public Sprite Creatur2Sprite;
    public GameObject Creatur2Prefab;
    public Sprite Creatur3Sprite;
    public GameObject Creatur3Prefab;
    public Sprite Creatur4Sprite;
    public GameObject Creatur4Prefab;
    public List<GameObject> GetObjsFromTypeOfSummon(RequestTypes[] types)
    {
        List<GameObject> objPrefabs = new List<GameObject>();
        foreach(RequestTypes type in types)
        {
            objPrefabs.Add(findObj(type));
        }
        return objPrefabs;
    }

    public GameObject findObj(RequestTypes type)
    {
        switch(type)
        {
            case RequestTypes.Creature1:
                 return Creatur1Prefab;
            case RequestTypes.Creature2:
                return Creatur2Prefab;
            case RequestTypes.Creature3:
                return Creatur3Prefab;
            case RequestTypes.Creature4:
                return Creatur4Prefab;
        }
        return null;
    }

    public Sprite findSprite(RequestTypes type)
    {
        switch (type)
        {
            case RequestTypes.Creature1:
                return Creatur1Sprite;
            case RequestTypes.Creature2:
                return Creatur2Sprite;
            case RequestTypes.Creature3:
                return Creatur3Sprite;
            case RequestTypes.Creature4:
                return Creatur4Sprite;
        }
        return null;
    }
}
