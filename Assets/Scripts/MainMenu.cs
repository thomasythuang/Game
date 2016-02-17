using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string startLevel;

    public string levelSelect;

    public void NewGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitGame()
    {
        Debug.Log("Exited game");
        Application.Quit();
    }
}
