using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    private GameController gameController;
	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!Globals.GetInstance()._gameOver && 
            GameObject.Find("Player").transform.position.y >= Camera.main.transform.position.y + Globals.GetInstance()._maxY / 2)
        {

            Vector3 destination = new Vector3(Camera.main.transform.position.x, Globals.GetInstance()._lastTile.transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, destination, 5 * Time.deltaTime);
        }
    }
}
