using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
    public GameObject PauseMenu;
	// Use this for initialization
	void Start () {
        Resume();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.P))
        {
            print(Time.timeScale);
            if (Time.timeScale == 0) { Resume(); }
            else { Pause(); }
        }
	}
    void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }
    void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
}

