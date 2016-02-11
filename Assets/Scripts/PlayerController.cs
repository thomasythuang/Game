using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    public bool selected;
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
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
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
                    Vector3 newPos = new Vector3(oldPos.x + 3, oldPos.y, 0);
                    Quaternion quat = new Quaternion();
                    CloneCharacter(health, newPos, quat);
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
    private GameObject CloneCharacter(int health, Vector3 pos, Quaternion quat)
    {
        GameObject clone = Instantiate(Resources.Load("Character"), pos, quat) as GameObject;
        PlayerController cloneController = clone.GetComponent<PlayerController>();
        cloneController.selected = false;
        cloneController.currentHealth = health;
        cloneController.initSprite(cloneController);
        return clone;
    }

    // This is needed to set the initial sprites when a new character is instantiated
    // Resources.LoadAll is too slow, so the resouces don't load properly if HealthChange is invoked at instantiation
    private void initSprite(PlayerController controller)
    {
        SpriteRenderer indicatorSpriteRenderer = this.transform.Find("Selected Indicator").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer.enabled = false;

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
    }

    private void DestroyCharacter()
    {
        Destroy(this.gameObject);
        CharacterDied.Invoke();
    }
}