using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RandomSummonCreations : MonoBehaviour
{
    public List<RequestTypes> mostersAllowedForRandomCreation;
    public float delayForRandomness = 4f;
    public float frequencyOfRandomSummons = 3f;

    float timeSinceLastRandomSummon;
    GameSummonManager gameSummonManager;

    public bool paused = false;
    public void Pause()
    {
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
        timeSinceLastRandomSummon = Time.time + delayForRandomness;
    }

        // Start is called before the first frame update
    void Start()
    {
        timeSinceLastRandomSummon = delayForRandomness;
        gameSummonManager = GetComponent<GameSummonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) { return; }
        if (Time.time > timeSinceLastRandomSummon + frequencyOfRandomSummons)
        {
            timeSinceLastRandomSummon = Time.time;
            SummonRandomCreature();
        }
    }

    public void SummonRandomCreature()
    {
        RequestTypes randomMonsterRequest = mostersAllowedForRandomCreation[Random.Range(0, mostersAllowedForRandomCreation.Count)];
        GameObject prefab = GetComponent<SummonLibrary>().findObj(randomMonsterRequest);
        GameObject SpawnLoc = gameSummonManager.GetRandomSpawn();
        Instantiate(prefab, SpawnLoc.transform.position, Quaternion.identity);
    }
}
