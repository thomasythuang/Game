using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool selected;
    public int maxHealth;
    public int currentHealth;
    public float moveSpeed;
    public float jumpHeight;
    private bool touchingSpikes;
    private float startTime;
    public string direction;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    public event Action HealthChange = delegate {};
    public event Action CharacterDied = delegate {};

	// Use this for initialization
	void Start () {      
        InfoTextScript infoTextScript = GameObject.Find("Info Text").GetComponent<InfoTextScript>();
        CharacterDied += infoTextScript.OnCharacterDied;
        this.gameObject.tag = "Character";
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

            if (direction != "right")
            {
                ChangeDirection("right");
            }
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (direction != "left")
            {
                ChangeDirection("left");
            }
		}

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (selected)
            {
                // Health dividing. Right now we're being brutal, and rounding down 3 -> 1 and 1
                if (currentHealth > 1)
                {
                    int health;
                    if (currentHealth == 4)
                    {
                        health = 2;
                        setHealth(2);
                    }
                    else
                    {
                        health = 1;
                        setHealth(1);
                    }
                    Vector2 oldPos = this.GetComponent<Rigidbody2D>().position;
                    Vector3 newPos;
                    if (direction == "left")
                    {
                        newPos = new Vector3(oldPos.x - 3, oldPos.y, 0);
                    }
                    else
                    {
                        newPos = new Vector3(oldPos.x + 3, oldPos.y, 0);
                    }
                    Quaternion quat = new Quaternion();
                    CloneCharacter(health, newPos, quat, direction);
                }
            }
        }

        if (touchingSpikes)
        {
            if (Time.time > startTime + 3)
            {
                if (currentHealth > 1)
                {
                    setHealth(currentHealth - 1);
                    startTime = Time.time;
                }
                else
                {
                    DestroyCharacter();
                }
            }
        }

        if (this.transform.position.y < -7)
        {
            DestroyCharacter();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            setHealth(currentHealth - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (currentHealth < 4)
            {
                setHealth(currentHealth + 1);
            }
        }
	}

    void ChangeDirection(string newDirection)
    {
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (newDirection == "left")
        {
            direction = "left";
            Sprite characterLeft = Resources.Load<Sprite>("character_left");
            spriteRenderer.sprite = characterLeft;
        }
        else
        {
            direction = "right";
            Sprite characterRight = Resources.Load<Sprite>("character_right");
            spriteRenderer.sprite = characterRight;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 11)
        {
            startTime = Time.time - 4;
            touchingSpikes = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 11)
        {
            touchingSpikes = false;
        }
    }


    // Change currentHealth to the given value, and invoke HealthChange to update the health sprite
    private void setHealth(int health)
    {
        if (health > 0)
        {
            currentHealth = health;
            HealthChange.Invoke();
        }
        else
        {
            DestroyCharacter();
        }
        
    }
    
    // Create a new character and set its health to half the value of the original
    private GameObject CloneCharacter(int health, Vector3 pos, Quaternion quat, string initialDirection)
    {
        GameObject clone = Instantiate(Resources.Load("Character"), pos, quat) as GameObject;
        PlayerController cloneController = clone.GetComponent<PlayerController>();
        cloneController.selected = false;
        cloneController.currentHealth = health;
        cloneController.direction = initialDirection;
        cloneController.initCharacterSprites(cloneController);
        return clone;
    }

    // This is needed to set the initial sprites when a new character is instantiated
    // Resources.LoadAll is too slow, so the resouces don't load properly if HealthChange is invoked at instantiation
    private void initCharacterSprites(PlayerController controller)
    {
        // Disable the selection sprite upon instantiation
        SpriteRenderer indicatorSpriteRenderer = this.transform.Find("Selected Indicator").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer.enabled = false;

        // Set the starting health to the appropriate amount
        SpriteRenderer healthSpriteRenderer = this.transform.Find("Health Display").GetComponent<SpriteRenderer>();
        Sprite[] healthSprites = Resources.LoadAll<Sprite>("life_hearts");
        int health = controller.currentHealth;
        if (health == 1)
        {
            healthSpriteRenderer.sprite = healthSprites[0];
        }
        else if (health == 2)
        {
            healthSpriteRenderer.sprite = healthSprites[1];
        }
        else if (health == 3)
        {
            healthSpriteRenderer.sprite = healthSprites[2];
        }
        else
        {
            healthSpriteRenderer.sprite = healthSprites[3];
        }

        controller.ChangeDirection(controller.direction);
    }

    private void DestroyCharacter()
    {
        Destroy(this.gameObject);
        InfoTextScript infoTextScript = GameObject.Find("Info Text").GetComponent<InfoTextScript>();
        infoTextScript.destroyedCharacters++;
        CharacterDied.Invoke();
    }

    
}