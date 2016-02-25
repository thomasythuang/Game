using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    // For each level button, specify the level number in the onClick section of the Unity Editor
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
