using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public bool done;
    public int direction; /* 1 - right, 2 - left*/

    private GameObject floor;
    private BoxCollider2D floorBC;

    private GameController gc;

    private Vector3 destination;

    private Rigidbody2D tileRB;
    private BoxCollider2D tileBC;

    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor");
        floorBC = floor.GetComponent<BoxCollider2D>();

        done = false;

        tileRB = GetComponent<Rigidbody2D>();
        tileBC = GetComponent<BoxCollider2D>();

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

        if (done == false)
        {
            //transform.position = Vector3.MoveTowards(transform.position, destination, 5 * Time.deltaTime);
            if (direction == 1)
            {
                tileRB.velocity = transform.right * 5;
            }
            else if (direction == 2)
            {
                tileRB.velocity = - transform.right * 5;
            }
            if ( tileBC.IsTouching(GameObject.Find("Player").GetComponent<BoxCollider2D>()) )
            {
                tileRB.velocity = new Vector2(0, 0);
                tileRB.bodyType = RigidbodyType2D.Dynamic;
                tileRB.gravityScale = 0.5f;
                done = true;
            }
        }
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameObject.Find("Player"))
        {
            tileRB.velocity = new Vector2(0, 0);
            done = true;
        }
    }*/
}
