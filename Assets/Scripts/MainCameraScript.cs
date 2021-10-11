using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

    public GameOverScreen GameOverScreen;
    int maxPlatform = 0;

    public void GameOver()
    {
        GameOverScreen.Setup(maxPlatform);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
