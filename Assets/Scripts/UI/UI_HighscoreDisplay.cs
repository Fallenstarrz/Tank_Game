using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HighscoreDisplay : MonoBehaviour
{
    public RectTransform highScoreLoc;
    public Text highScoreNum;
    public Vector3 singlePlayerPos = new Vector3(-767, 390, 0);
    public Vector3 multiPlayerPos = new Vector3(0,0,0);

	// Use this for initialization
	void Start ()
    {
        highScoreLoc = GetComponent<RectTransform>();
        if (GameManager.instance.isMultiplayer == true)
        {
            highScoreLoc.anchoredPosition = singlePlayerPos;
        }
        else
        {
            highScoreLoc.anchoredPosition = multiPlayerPos;
        }
        highScoreNum.text = GameManager.instance.highScore.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.instance.highScore > int.Parse(highScoreNum.text.ToString()))
        {
            highScoreNum.text = GameManager.instance.highScore.ToString();
        }
	}
}
