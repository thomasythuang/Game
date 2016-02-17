using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    // This is a pretty hacky way- will look later for
    // a way to load levels dynamically

    public string level0;
    public string level1;
    public string level2;
    public string level3;

    public void LoadLevel0()
    {
        SceneManager.LoadScene(level0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(level1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(level2);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(level3);
    }
}
