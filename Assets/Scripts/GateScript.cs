using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {

    public bool isOpen;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void disableLock ()
    {
        this.transform.Find("Lock").GetComponent<SpriteRenderer>().enabled = false;
        this.transform.Find("R Wall").GetComponent<BoxCollider2D>().enabled = false;
        this.transform.Find("R Wall").Find("L Wall").GetComponent<BoxCollider2D>().enabled = false;
        this.transform.Find("R Wall").Find("Top Wall").GetComponent<BoxCollider2D>().enabled = false;
        this.isOpen = true;
    }

    public void enableLock ()
    {
        this.transform.Find("Lock").GetComponent<SpriteRenderer>().enabled = true;
        this.transform.Find("R Wall").GetComponent<BoxCollider2D>().enabled = true;
        this.transform.Find("R Wall").Find("L Wall").GetComponent<BoxCollider2D>().enabled = true;
        this.transform.Find("R Wall").Find("Top Wall").GetComponent<BoxCollider2D>().enabled = true;
        this.isOpen = false;
    }
}
