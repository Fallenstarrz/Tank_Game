using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public TankData data;

	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponent<Text>();
        data = GetComponentInParent<TankData>();
        setUIScore();
	}
	
	// Update is called once per frame
	void Update ()
    {
        setUIScore();
	}

    // set score text to the players current score
    void setUIScore()
    {
        scoreText.text = data.score.ToString();
    }
}
