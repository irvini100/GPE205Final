using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : MonoBehaviour {
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float bulletforce = 10;

	// Use this for initialization
	void Start () {
        currentAmmo = maxAmmo;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}
    public void Shoot()
    {
        //This will create a game object copied from a prefab.
        currentAmmo--;
        GameObject newbullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        newbullet.GetComponent<Rigidbody>().AddForce(newbullet.transform.forward*bulletforce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Tank1")
        {
            ScoreManager.instance.AddScore();
        }
    }
}
