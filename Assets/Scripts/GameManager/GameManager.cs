using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Player Holders
    public GameObject player1;
    public GameObject player2;

    // Player Lives
    public int player1Lives;
    public int player2Lives;
    public int playerLivesMax;

    // Player Scores
    public int player1Score;
    public int player2Score;

    // Player Controls Selection
    public int player1Controller;
    public int player2Controller;

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
        resetGame();
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
                player1Lives -= 1;
                if (player1Lives >= 0)
                {
                    player1 = respawnPlayer();
                    setUpPlayer1();
                }
            }
            if (isMultiplayer == true)
            {
                if (player2 == null)
                {
                    player2Lives -= 1;
                    if (player2Lives >= 0)
                    {
                        player2 = respawnPlayer();
                        setUpPlayer2();
                    }
                }
            }
        }
        setHighScore();
        checkLoss();
	}

    // Reset game to defaults
    void resetGame()
    {
        player1Score = 0;
        player2Score = 0;
        player1Lives = playerLivesMax;
        player2Lives = playerLivesMax;
        characterSpawns.Clear();
        numAICurrent = 0;
        players.Clear();
        aiUnits.Clear();
        spawnedPickups.Clear();
    }

    // check if both players are dead
    void checkLoss()
    {
        if (isMultiplayer == true)
        {
            if (player1Lives < 0 && player2Lives < 0)
            {
                resetGame();
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (player1Lives < 0)
            {
                resetGame();
                SceneManager.LoadScene(0);
            }
        }
    }

    // Set up player 1's tank data variables and controller variables
    void setUpPlayer1()
    {
        TankData playerData = player1.GetComponent<TankData>();
        playerData.score = player1Score;
        playerData.lives = player1Lives;
        playerData.myName = "Player1";
        Controller_Player playerController = player1.GetComponent<Controller_Player>();
        if (player1Controller == 0)
        {
            playerController.selectedController = Controller_Player.controlType.wasd;
        }
        else if (player1Controller == 1)
        {
            playerController.selectedController = Controller_Player.controlType.arrows;
        }
        else if (player1Controller == 2)
        {
            playerController.selectedController = Controller_Player.controlType.controller;
        }
    }

    // Set up player 2's tank data variables and controller variables
    void setUpPlayer2()
    {
        TankData playerData = player2.GetComponent<TankData>();
        playerData.score = player2Score;
        playerData.lives = player2Lives;
        playerData.myName = "Player2";
        Controller_Player playerController = player2.GetComponent<Controller_Player>();
        if (player2Controller == 0)
        {
            playerController.selectedController = Controller_Player.controlType.wasd;
        }
        else if (player2Controller == 1)
        {
            playerController.selectedController = Controller_Player.controlType.arrows;
        }
        else if (player2Controller == 2)
        {
            playerController.selectedController = Controller_Player.controlType.controller;
        }
    }

    // Set high scores in playerPrefs & in gameManager
    void setHighScore()
    {
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

    // Final update of high score, so no data is lost when game is closed
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
