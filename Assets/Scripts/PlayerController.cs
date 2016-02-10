using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;

  public void RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

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

    if (Input.GetKeyDown(KeyCode.R))
    {
      RestartLevel();
    }
	}
}