using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonLibrary : MonoBehaviour
{
    public GameObject CubePrefab;
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
            case RequestTypes.Cube:
                 return CubePrefab;
        }
        return null;
    }
}
