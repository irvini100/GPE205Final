using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    //Setup our singleton.

    private static GameManager inst;
    public static GameManager Instance

    {
        get
        {
            if (inst == null)
            {
                GameObject go = new GameObject("GameManager"); go.AddComponent<GameManager>
();
            }
            return inst;
        }
    }
    public List<TankData> tanks;
    public List<InputController> players;
   



    void Awake()
    {
        //If singleton already exists.
        if (inst != null)
        {
            //Self-destruct
            Destroy(gameObject);
        }
        else
        {
            //Otherwise, I am the instance.
            
            inst = this;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
