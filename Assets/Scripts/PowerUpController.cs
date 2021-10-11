using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController  :  MonoBehaviour {
    public List<Powerup> powerups;
    public TankData Pawn;

    public void Add (Powerup powerup)
    {
        powerup.OnActivate (Pawn);
        if (!powerup.isPermanent)
        {
            powerups.Add(powerup);
        }
    }
    

	// Use this for initialization
	void Start () {
        powerups = new List<Powerup>();
	}
	
	// Update is called once per frame
	void Update () {

        List<Powerup> expiredPowerups = new List<Powerup>();
        //Loop through all the powers in the list.
        foreach (Powerup power in expiredPowerups)
        {
            power.duration -= Time.deltaTime;

            if (power.duration <= 0)
            {
                expiredPowerups.Add(power);
            }
        }

        foreach (Powerup power in expiredPowerups)
        {
            power.OnDeactivate(Pawn);
            powerups.Remove(power);
        }
        expiredPowerups.Clear();
		
	}
}
