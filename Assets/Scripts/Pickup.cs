using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;
    public AudioClip feedback;
    private Transform tf;
    public GameObject pickuPrefab;
    public float spawnDelay;
    private float nextSpawnTime;

    public void OnTriggerEnter(Collider other)
    {//Variable to store other object's PowerupController-if it has one.
        PowerUpController powCon = other.GetComponent<PowerUpController>();
        if (powCon != null)
        {
            powCon.Add(powerup);

            //Play feedback if it is set.
            if (feedback != null)
            {
                AudioSource.PlayClipAtPoint(feedback, tf.position, 1.0f);
            }

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;

    }

    // Update is called once per frame
    void Update()
    {
        //If it is time to spawn a pickup.
        if (Time.time > nextSpawnTime)
        {
            Instantiate(pickuPrefab, tf.position, Quaternion.identity);
            nextSpawnTime = Time.time + spawnDelay;
        }

        else
        {
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}

