using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public float moveSpeed;
    public float jumpHeight;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    public event Action HealthChange = delegate {};
    public event Action CharacterDied = delegate {};

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
    }
	
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}

        if (Input.GetKeyDown(KeyCode.Space)) {
                Vector2 oldPos = this.GetComponent<Rigidbody2D>().position;
                Vector3 newPos = new Vector3(oldPos.x + 3, oldPos.y, 0);
                Quaternion quat = new Quaternion();
                UnityEngine.Object.Instantiate(this, newPos, quat);
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