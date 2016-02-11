using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
    }
	
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
            if (characters.Length > 1)
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    PlayerController thisController = characters[i].GetComponent<PlayerController>();
                    if (thisController.selected)
                    {
                        if (i < (characters.Length - 1))
                        {
                            SelectCharacter(characters[i + 1]);
                            DeselectCharacter(characters[i]);
                            break;
                        }
                        else
                        {
                            SelectCharacter(characters[0]);
                            DeselectCharacter(characters[i]);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SelectCharacter(GameObject character)
    {
        PlayerController controller = character.GetComponent<PlayerController>();
        controller.selected = true;
        SpriteRenderer indicatorSpriteRenderer = character.transform.Find("Selected Indicator").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer.enabled = true;
    }

    private void DeselectCharacter(GameObject character)
    {
        PlayerController controller = character.GetComponent<PlayerController>();
        controller.selected = false;
        SpriteRenderer indicatorSpriteRenderer = character.transform.Find("Selected Indicator").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer.enabled = false;
    }
}
