using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public bool done;

    private GameObject floor;
    private BoxCollider2D floorBC;

    private GameController gc;

    private Vector3 destination;

    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor");
        floorBC = floor.GetComponent<BoxCollider2D>();

        done = false;

        gc = GameObject.Find("GameController").GetComponent<GameController>();

        if (gc.i == 0)
        {
            destination = new Vector3(floor.transform.position.x, floor.transform.position.y , 0);
        }
        else
        {
            BoxCollider2D lastTileBC = gc.lastTile.GetComponent<BoxCollider2D>();
            destination = new Vector3(floor.transform.position.x, gc.lastTile.transform.position.y , 0);
        }
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, destination, 5 * Time.deltaTime);

        if (transform.position == destination)
        {
            done = true;
        }

    }
}
