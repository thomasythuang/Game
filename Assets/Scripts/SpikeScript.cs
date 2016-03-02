using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.DamageCharacter();;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.DamageCharacter();
        }
    }
}
