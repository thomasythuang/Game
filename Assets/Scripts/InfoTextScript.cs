using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoTextScript : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {
    GameObject character = GameObject.Find("Character");
    PlayerController playerController = character.GetComponent<PlayerController>();
    playerController.CharacterDied += OnCharacterDied;
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCharacterDied()
  {
    GameObject infoTextObject = this.gameObject;
    infoTextObject.GetComponent<Text>().text = string.Format("You died! \nPress R to restart the level");
  }
}
