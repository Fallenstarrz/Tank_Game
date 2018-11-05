using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int numAIToSpawn;
    public int numAICurrent;

    public GameObject playerPrefab;
    public GameObject aiPrefab;

    public GameObject characters;

    public List<TankData> players;
    public List<TankData> aiUnits;

    public List<Transform> characterSpawns;
    public List<GameObject> spawnedPickups;

    // Created temporarily
    public GameObject player1;

    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        respawnAI();
        if (player1 == null)
        {
            player1 = respawnPlayer();
        }
	}

    // Respawn AIs
    void respawnAI()
    {
        // Maybe use a for loop instead
        // TODO: This loop is temporary
        while (numAICurrent < numAIToSpawn)
        {
            int randomNum = Random.Range(0, characterSpawns.Count-1);
            Transform locationToSpawn = characterSpawns[randomNum];
            GameObject newAI = Instantiate(aiPrefab, locationToSpawn);
            newAI.transform.SetParent(characters.transform);
            setAiWaypoints(newAI, locationToSpawn);
            numAICurrent++;
        }
    }

    // Set waypoints on newly spawned AI
    void setAiWaypoints(GameObject spawnedAI, Transform spawnLocation)
    {
        foreach (Transform point in spawnLocation.gameObject.GetComponentInParent<Room>().waypoints)
        {
            spawnedAI.GetComponent<Controller_AI>().waypoints.Add(point);
        }
    }

    // Respawn players
    GameObject respawnPlayer()
    {
        int randomNum = Random.Range(0, characterSpawns.Count-1);
        Transform spawnLocation = characterSpawns[randomNum];
        GameObject player = Instantiate(playerPrefab, spawnLocation);
        player.transform.SetParent(characters.transform);
        return player;
    }
}
