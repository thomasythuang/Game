using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour {

  public void RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown(KeyCode.R))
    {
      RestartLevel();
    }
  }
}
