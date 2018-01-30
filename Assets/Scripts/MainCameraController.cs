using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    private GameController gc;
	// Use this for initialization
	void Start () {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Player").transform.position.y >= Camera.main.transform.position.y + Globals.maxY / 2)
        {

            Vector3 destination = new Vector3(Camera.main.transform.position.x, gc.lastTile.transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, destination, 5 * Time.deltaTime);
        }
    }
}
