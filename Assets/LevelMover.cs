using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMover : MonoBehaviour
{
    public static LevelMover instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LevelMover[] movers = FindObjectsOfType<LevelMover>();
        if (movers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void LoadNextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
