using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCulling : MonoBehaviour
{
    public Camera cam;
    public RaycastHit[] hits;
    public List<GameObject> objectsHit;
    public List<GameObject> objectsToRemove;

	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();
        objectsHit = new List<GameObject>();
        objectsToRemove = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        hits = Physics.RaycastAll(transform.position, transform.forward, 8.0f);
        // Set anything in the objects to remove layer to layer 0 (default) and clear the list
        foreach (GameObject removable in objectsToRemove)
        {
            setDefaultLayer(removable);
            objectsHit.Remove(removable);
        }
        // Switch culling masks, primary use is multiplayer, so one player doesn't make walls disappear for the other one
        if (objectsToRemove.Count != 0)
        {
            objectsToRemove.Clear();
            cam.cullingMask = ~(1<<9);
        }
        else
        {
            cam.cullingMask |= (1<<9);
        }

        // Add objects to list of objects to cull
        foreach (RaycastHit hit in hits)
        {
            setCullingLayer(hit.transform.gameObject);
            if (!objectsHit.Contains(hit.transform.gameObject))
            {
                objectsHit.Add(hit.transform.gameObject);
            }
        }

        // Check if object has left hits array
        foreach (GameObject objHit in objectsHit)
        {
            if (!ArrayList.Adapter(hits).Contains(objHit))
            {
                if (!objectsToRemove.Contains(objHit))
                {
                    objectsToRemove.Add(objHit);
                }
            }
        }
    }

    // Set layer of object to CullMe (9)
    void setCullingLayer(GameObject obj)
    {
        obj.layer = 9;
    }

    // set layer of object to default (0)
    void setDefaultLayer(GameObject obj)
    {
        obj.layer = 0;
    }
}
