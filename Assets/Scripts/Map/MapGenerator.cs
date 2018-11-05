using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int cols;
    private float roomWidth = 30.0f;
    private float roomHeight = 30.0f;
    public List<GameObject> roomPrefabs;
    private Room[,] myMap;

    public int seedValue;

    public enum mapType
    {
        mapOfTheDay,
        randomMap,
        seededMap
    }
    public mapType mapTypeSelection;

    // Use this for initialization
    void Start()
    {
        generateMap();
    }

    public void generateMap()
    {
        switch (mapTypeSelection)
        {
            case mapType.mapOfTheDay:
                {
                    // Set seed of mapOfTheDay to today
                    int now = getDateAsInt();
                    UnityEngine.Random.InitState(now);
                    break;
                }
            case mapType.randomMap:
                {
                    // Do some stuff
                    break;
                }
            case mapType.seededMap:
                {
                    // Do some stuff
                    UnityEngine.Random.InitState(seedValue);
                    break;
                }
        }
        myMap = new Room[cols, rows];

        // Generate the map
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                float xPos = roomWidth * j;
                float zPos = roomHeight * i;
                Vector3 newPos = new Vector3(xPos, 0.0f, zPos);

                // Create room at location
                GameObject newRoom = Instantiate(getRandomRoom(), newPos, Quaternion.identity);

                // set parent
                newRoom.transform.parent = this.transform;

                // give it a name for heirarchy
                newRoom.name = "Room: " + j + " , " + i;

                // set doors
                setDoors(newRoom, i, j);
            }
        }
        // Reset seed
        UnityEngine.Random.InitState(System.Environment.TickCount);
    }

    // set doors in room
    void setDoors(GameObject roomToChange, int currentI, int currentJ)
    {
        Room tempRoom = roomToChange.GetComponent<Room>();
        // Handle Rows
        if (currentI == 0)
        {
            tempRoom.northDoor.SetActive(false);
        }
        else if (currentI == rows - 1)
        {
            tempRoom.southDoor.SetActive(false);
        }
        else
        {
            tempRoom.southDoor.SetActive(false);
            tempRoom.northDoor.SetActive(false);
        }
        // Handle Columns
        if (currentJ == 0)
        {
            tempRoom.eastDoor.SetActive(false);
        }
        else if (currentJ == cols - 1)
        {
            tempRoom.westDoor.SetActive(false);
        }
        else
        {
            tempRoom.westDoor.SetActive(false);
            tempRoom.eastDoor.SetActive(false);
        }
    }

    // set day month and year as string and concactinate them then turn that into an int to be used for map of the day
    int getDateAsInt()
    {
        string day = DateTime.Now.Day.ToString();
        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string nowSeed = day + month + year;
        int currentSeed = int.Parse(nowSeed);

        return currentSeed;
    }

    // get a random number, then get that element of the list to use as currentRoom
    public GameObject getRandomRoom()
    {
        int randomSelection = UnityEngine.Random.Range(0, roomPrefabs.Count);
        GameObject currentRoom = roomPrefabs[randomSelection];
        return currentRoom;
    }
}
