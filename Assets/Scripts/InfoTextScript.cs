using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoTextScript : MonoBehaviour
{
    public int destroyedCharacters = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCharacterDied()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        if (characters == null || characters.Length == (0 + destroyedCharacters))
        {
            GameObject infoTextObject = this.gameObject;
            infoTextObject.GetComponent<Text>().text = string.Format("You died! \nPress R to restart the level");
        }
        destroyedCharacters--;
    }
}
