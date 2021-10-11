using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage = 25;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TankData>())
        {
            collision.gameObject.GetComponent<TankData>().TakeDamage(damage);
        } 
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
