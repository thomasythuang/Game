using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public GameObject gate;
    private GateScript gateScript;

    // Use this for initialization
    void Start () {
        gateScript = gate.GetComponent<GateScript>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
        gateScript.disableLock();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
        gateScript.enableLock();
    }

}
