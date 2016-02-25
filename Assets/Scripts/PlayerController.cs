using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool selected;
    public int maxHealth;
    public int currentHealth;
    public float moveSpeed;
    public float jumpHeight;
    private float startTime;
    public string direction;
    private bool invulnerable;
    public float invulnerableDuration;

    public AudioSource jumpSoundEffect;
    public AudioSource damageSoundEffect;
    public AudioSource projectionSoundEffect;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsHazard;
    private bool grounded;

    public event Action HealthChange = delegate { };
    public event Action CharacterDied = delegate { };

    // Use this for initialization
    void Start() {
        InfoTextScript infoTextScript = GameObject.Find("Info Text").GetComponent<InfoTextScript>();
        CharacterDied += infoTextScript.OnCharacterDied;
        this.gameObject.tag = "Character";
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        grounded = grounded || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsHazard);
    }

	// Update is called once per frame
	void Update () {
        Rigidbody2D characterRigidbody = this.GetComponent<Rigidbody2D>();

        if (characterRigidbody.IsSleeping())
        {
            characterRigidbody.WakeUp();
        }

		if (selected && Input.GetKeyDown(KeyCode.UpArrow) && grounded) {
			characterRigidbody.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            jumpSoundEffect.Play();
		}

		if (selected && Input.GetKey(KeyCode.RightArrow)) {
			characterRigidbody.velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (direction != "right")
            {
                ChangeDirection("right");
            }
		}

		if (selected && Input.GetKey(KeyCode.LeftArrow)) {
            characterRigidbody.velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (direction != "left")
            {
                ChangeDirection("left");
            }
		}

        if (selected && Input.GetKeyDown(KeyCode.Space)) {
            // Health dividing. Right now we're being brutal, and rounding down 3 -> 1 and 1
            if (currentHealth > 1)
            {
                int health;
                if (currentHealth == 4)
                {
                    health = 2;
                    SetHealth(2);
                }
                else
                {
                    health = 1;
                    SetHealth(1);
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

        if (invulnerable && Time.time > startTime + invulnerableDuration)
        {
            invulnerable = false;
        }

        if (this.transform.position.y < -7)
        {
            DestroyCharacter();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            SetHealth(currentHealth - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            SetHealth(currentHealth + 1);
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

    public void DamageCharacter()
    {
        if (!invulnerable)
        {
            damageSoundEffect.Play();
            invulnerable = true;
            startTime = Time.time;
            SetHealth(currentHealth - 1);
            StartCoroutine(BlinkSmooth(4f, invulnerableDuration, Color.red));
        }
    }

    // Change currentHealth to the given value, and invoke HealthChange to update the health sprite
    public void SetHealth(int health)
    {
        if (health > 0 && health < 5)
        {
            currentHealth = health;
            HealthChange.Invoke();
        }
        else if (health > 4)
        {
            return;
        }
        else
        {
            DestroyCharacter();
        }
        
    }
    
    // Create a new character and set its health to half the value of the original
    private void CloneCharacter(int health, Vector3 pos, Quaternion quat, string initialDirection)
    {
        projectionSoundEffect.Play();
        GameObject clone = Instantiate(Resources.Load("Character"), pos, quat) as GameObject;
        PlayerController cloneController = clone.GetComponent<PlayerController>();
        cloneController.currentHealth = health;
        cloneController.direction = initialDirection;
        cloneController.initCharacterSprites(cloneController);
        SelectCharacter(clone);
        DeselectCharacter(this.gameObject);
    }

    // This is needed to set the initial sprites when a new character is instantiated
    // Resources.LoadAll is too slow, so the resouces don't load properly if HealthChange is invoked at instantiation
    private void initCharacterSprites(PlayerController controller)
    {
        // Disable the selection sprite upon instantiation
        SpriteRenderer indicatorSpriteRenderer = this.transform.Find("Selected Indicator").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer.enabled = true;

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

    IEnumerator BlinkSmooth(float timeScale, float duration, Color blinkColor)
    {
        var material = GetComponent<SpriteRenderer>().material;
        var elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            material.SetColor("_BlinkColor", blinkColor);

            blinkColor.a = Mathf.PingPong(elapsedTime * timeScale, 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // revert to our standard sprite color
        blinkColor.a = 0f;
        material.SetColor("_BlinkColor", blinkColor);
    }

    private void SelectCharacter(GameObject character)
    {
        PlayerController controller = character.GetComponent<PlayerController>();
        controller.selected = true;
        SpriteRenderer indicatorSpriteRenderer = character.transform.Find("Selected Indicator").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer.enabled = true;
    }

    private void DeselectCharacter(GameObject character)
    {
        PlayerController controller = character.GetComponent<PlayerController>();
        controller.selected = false;
        SpriteRenderer indicatorSpriteRenderer = character.transform.Find("Selected Indicator").GetComponent<SpriteRenderer>();
        indicatorSpriteRenderer.enabled = false;
    }

    private void DestroyCharacter()
    {
        LevelScript levelScript = GameObject.Find("Level Manager").GetComponent<LevelScript>();
        levelScript.deathSoundEffect.Play();
        Destroy(this.gameObject);
        InfoTextScript infoTextScript = GameObject.Find("Info Text").GetComponent<InfoTextScript>();
        infoTextScript.destroyedCharacters++;
        CharacterDied.Invoke();
    }

    
}