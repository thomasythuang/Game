using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {

    public bool verticalMovement;

    public float moveInterval;
    public float ymin;
    public float ymax;

    private bool verticalDirection;

	// Use this for initialization
	void Start () {
        verticalDirection = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (verticalMovement)
        {
            if (this.transform.position.y < ymin)
            {
                verticalDirection = true;
            }
            else if (this.transform.position.y > ymax)
            {
                verticalDirection = false;
            }
            if (verticalDirection)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + moveInterval, this.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - moveInterval, this.transform.position.z);
            }
        }
	}
}
