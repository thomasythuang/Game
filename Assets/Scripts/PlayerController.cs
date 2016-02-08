using UnityEngine;
using System.Collections;

public class CharScript : MonoBehaviour {

	public Vector2 JumpVelocity;
	public Vector2 RunVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			this.GetComponent<Rigidbody2D>().AddForce(JumpVelocity, ForceMode2D.Impulse);
		}

		if (Input.GetButtonDown("Horizontal")) {
			this.GetComponent<Rigidbody2D>().AddForce(RunVelocity, ForceMode2D.Force);
		}
	}
}
