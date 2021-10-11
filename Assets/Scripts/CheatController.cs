using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatController : MonoBehaviour {
    public PowerUpController powCon;
    public Powerup cheatPowerup;

	// Use this for initialization
	void Start () {
        if (powCon == null)
        {
            powCon = gameObject.GetComponent<PowerUpController>();
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.H) && Input.GetKey (KeyCode.U) && Input.GetKeyDown(KeyCode.E))
        {
            //Add our powerup to the tank.
            powCon.Add(cheatPowerup);
        }
	}
}
