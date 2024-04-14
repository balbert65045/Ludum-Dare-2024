using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public void LoadLevel(string levelToLoad)
    {
        LevelMover.instance.LoadLevel(levelToLoad);
    }

    public void ReloadLevel()
    {
        LevelMover.instance.ReloadLevel();
    }

    public void LoadNextLevel()
    {
        LevelMover.instance.LoadNextLevel();

    }
}
