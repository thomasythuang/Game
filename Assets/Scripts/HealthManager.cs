using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

    public Sprite[] healthSprites;
  
	// Use this for initialization
    void Start () {
        healthSprites = Resources.LoadAll<Sprite>("life_hearts");

        PlayerController playerController = this.transform.parent.gameObject.GetComponent<PlayerController>();
        playerController.HealthChange += OnHealthChange;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnHealthChange()
    {
        PlayerController playerController = this.transform.parent.gameObject.GetComponent<PlayerController>();
        int health = playerController.currentHealth;
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        if (health == 1)
        {
            spriteRenderer.sprite = healthSprites[0];
        }
        else if (health == 2)
        {
            spriteRenderer.sprite = healthSprites[1];
        }
        else if (health == 3)
        {
            spriteRenderer.sprite = healthSprites[2];
        }
        else
        {
            spriteRenderer.sprite = healthSprites[3];
        }
    }
}
