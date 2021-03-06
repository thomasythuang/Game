﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour {

    public AudioSource deathSoundEffect;

    // Use this for initialization
    void Start () {
	
    }
	
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Tab) || (Input.GetKeyDown(KeyCode.Q)))
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
                    if (i == (characters.Length - 1))
                    {
                        SelectCharacter(characters[0]);
                        break;
                    }
                }
            }
            else if (characters.Length == 1)
            {
                if (!characters[0].GetComponent<PlayerController>().selected)
                {
                    SelectCharacter(characters[0]);
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
