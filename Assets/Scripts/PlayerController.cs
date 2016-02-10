using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;

  public event Action CharacterDied = delegate {};

	// Use this for initialization
	void Start () {
    
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
	}
}