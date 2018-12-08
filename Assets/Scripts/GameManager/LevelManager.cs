using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Camera camP1;
    public Camera camP2;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        // if either players lives are 0 switch to full screen on the remaining camera's pov
        // get camera of player 1 and player 2
        if (GameManager.instance.player1 != null)
        {
            camP1 = GameManager.instance.player1.GetComponentInChildren<Camera>();
        }
        // if multiplayer is enabled
        // set transforms of those players cameras
        if (GameManager.instance.isMultiplayer == true)
        {
            if (GameManager.instance.player2 != null)
            {
                camP2 = GameManager.instance.player2.GetComponentInChildren<Camera>();
            }
            setCameraSplitscreen();
        }
        else
        {
            setCameraFullscreen(camP1);
        }
    }

    // set up split screen
    void setCameraSplitscreen()
    {
        if (camP1 != null)
        {
            camP1.rect = new Rect(0, 0.5f, 1, 0.5f);
        }
        if (camP2 != null)
        {
            camP2.rect = new Rect(0, 0, 1, 0.5f);
        }
    }

    // set up full screen
    void setCameraFullscreen(Camera cam)
    {
        cam.rect = new Rect(0, 0, 1, 1);
    }
}
