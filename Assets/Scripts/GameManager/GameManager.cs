using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int numAIToSpawn;
    public int numAICurrent;

    public GameObject playerPrefab;
    public GameObject aiPrefab;

    public Transform charactersHolder;
    public Transform pickupsHolder;

    public List<TankData> players;
    public List<TankData> aiUnits;

    public List<Transform> characterSpawns;
    public List<GameObject> spawnedPickups;

    public MapGenerator mapMaker;
    public UI_LoadSettings settingsLoader;

    public int scorePerKill;

    // Settings
    public float sfxVol;
    public float musicVol;
    public bool isMultiplayer;
    public int seedNum;
    public int mapMode;
    public int highScore;

    // Created temporarily
    public GameObject player1;

    // Use this for initialization
    void Awake()
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
        if (settingsLoader != null)
        {
            settingsLoader.loadSettings();
        }
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!characterSpawns.Count.Equals(0))
        {
            respawnAI();
        }
        if (!characterSpawns.Count.Equals(0))
        {
            if (player1 == null)
            {
                player1 = respawnPlayer();
            }
        }
        foreach (TankData person in players)
        {
            if (person.score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", person.score);
                highScore = person.score; // Don't forget to save this when both players are out of lives
            }
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
            newAI.transform.SetParent(charactersHolder);
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
        player.transform.SetParent(charactersHolder);
        return player;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
