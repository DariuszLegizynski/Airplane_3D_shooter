using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleParallax : MonoBehaviour {

    [SerializeField]
    GameObject parallexTarget;

    [SerializeField]
    public float tileHeight;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentTile = transform.GetChild(i).gameObject;

            if (parallexTarget.transform.position.z - currentTile.transform.position.z >= tileHeight)
            {
                currentTile.transform.position = new Vector3(
                    currentTile.transform.position.x,
                    currentTile.transform.position.y,
                    currentTile.transform.position.z + transform.childCount * tileHeight);
            }
        }
    }
}
