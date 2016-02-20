using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    // For each level button, specify the level number in the onClick section of the Unity Editor
    public void LoadLevel(int num)
    {
        SceneManager.LoadScene("Level " + num.ToString());
    }
}
