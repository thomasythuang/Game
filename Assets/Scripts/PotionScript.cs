using UnityEngine;
using System.Collections;

public class PotionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Character")
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.SetHealth(playerController.currentHealth + 1);
            Destroy(this.gameObject);
        }
    }
}
