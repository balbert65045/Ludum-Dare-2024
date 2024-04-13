using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSummonCreations : MonoBehaviour
{
    public List<RequestTypes> mostersAllowedForRandomCreation;
    public float delayForRandomness = 4f;
    public float frequencyOfRandomSummons = 3f;

    float timeSinceLastRandomSummon;
    GameSummonManager gameSummonManager;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastRandomSummon = delayForRandomness;
        gameSummonManager = GetComponent<GameSummonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeSinceLastRandomSummon + frequencyOfRandomSummons)
        {
            timeSinceLastRandomSummon = Time.time;
            RequestTypes randomMonsterRequest = mostersAllowedForRandomCreation[Random.Range(0, mostersAllowedForRandomCreation.Count)];
            GameObject prefab = GetComponent<SummonLibrary>().findObj(randomMonsterRequest);
            GameObject SpawnLoc = gameSummonManager.GetRandomSpawn();
            Instantiate(prefab, SpawnLoc.transform.position, Quaternion.identity);
        }
    }
}
