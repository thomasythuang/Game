using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

  public int maxHealth;
  public int currentHealth;
  public float moveSpeed;
  public float jumpHeight;

  public event Action HealthChange = delegate {};
  public event Action CharacterDied = delegate {};

	// Use this for initialization
	void Start () {
    currentHealth = maxHealth;
  }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}

    if (this.transform.position.y < -7)
    {
      Destroy(this.gameObject);
      CharacterDied.Invoke();
    }

    if (Input.GetKeyDown(KeyCode.LeftBracket))
    {
      currentHealth--;
      if (currentHealth > 0)
      {
        HealthChange.Invoke();
      }
      else
      {
        DestroyThisCharacter();
      }
    }
    if (Input.GetKeyDown(KeyCode.RightBracket))
    {
      if (currentHealth < 4)
      {
        currentHealth++;
        HealthChange.Invoke();
      }
    }
	}

  private void DestroyThisCharacter()
  {
    Destroy(this.gameObject);
    CharacterDied.Invoke();
  }
}