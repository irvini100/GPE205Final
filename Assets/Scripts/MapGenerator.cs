
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public int rows;
    public int cols;
    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    public GameObject[] gridPrefabs;
    private Room[,] grid;
    public int mapSeed;
    private bool isMapOfTheDay;
    private Transform tf;


    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
        
    }

    //Use this for initialization
    void Start()
    {
        tf = GetComponent<Transform>();
        //Generate grid
        UnityEngine.Random.InitState(mapSeed);
        GenerateGrid();

        if (isMapOfTheDay)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }
    }

    void Update()
    {
        tf.position = tf.position + Vector3.up;
    }

    public void GenerateGrid()
    {
        grid = new Room[rows, cols];
        //For each grid row...
        for (int i = 0; i < rows; i++)
        {//For each column in that row...
            for (int j = 0; j < cols; j++)
            {
                float xPosition = roomWidth * j;
                float zPosition = roomWidth * i;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);
                GameObject tempRoomObj = Instantiate(RandomRoomPreFab(), newPosition, Quaternion.identity) as GameObject;
                tempRoomObj.transform.parent = this.transform;
                tempRoomObj.name = "Room_" + j + "," + i;
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                grid[j, i] = tempRoom;
                if (i == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if(i == rows-1){
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                if (j == 0){
                    tempRoom.doorEast.SetActive(false);
                }
                else if(j == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
                grid[j, i] = tempRoom;
            }
        }
    }
	public GameObject RandomRoomPreFab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

}
