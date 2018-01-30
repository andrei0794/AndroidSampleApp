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

    private float speed;
    private int last_count;

    private GameObject tower;
    private Rigidbody2D towerRB;

    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor");
        floorBC = floor.GetComponent<BoxCollider2D>();

        done = false;

        tileRB = GetComponent<Rigidbody2D>();
        tileBC = GetComponent<BoxCollider2D>();

        gc = GameObject.Find("GameController").GetComponent<GameController>();

        if (Globals.tileCounter == 0)
        {
            destination = new Vector3(floor.transform.position.x, floor.transform.position.y , 0);
        }
        else
        {
            BoxCollider2D lastTileBC = gc.lastTile.GetComponent<BoxCollider2D>();
            destination = new Vector3(floor.transform.position.x, gc.lastTile.transform.position.y , 0);
        }

        speed = 2;
        last_count = 0;

        tower = GameObject.Find("Tower");
        towerRB = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Globals.tileCounter > last_count + 5)
        {
            last_count = Globals.tileCounter;
            speed = speed + 0.4f;
        }

        if (done == false && !Globals.gameOver)
        {
            //transform.position = Vector3.MoveTowards(transform.position, destination, 5 * Time.deltaTime);
            if (direction == 1)
            {
                tileRB.velocity = transform.right * Random.Range(speed - 1, speed + 1);
            }
            else if (direction == 2)
            {
                tileRB.velocity = - transform.right * Random.Range(speed - 1, speed + 1);
            }
            if ( tileBC.IsTouching(GameObject.Find("Player").GetComponent<BoxCollider2D>()) )
            {
                tileRB.velocity = Vector2.zero;
                //tileRB.angularVelocity = 0f;
                //tileRB.bodyType = RigidbodyType2D.Dynamic;
                //tileRB.velocity = Vector2.zero;
                //tileRB.angularVelocity = 0f;
                //tileRB.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                //tileRB.gravityScale = 1;
                //tileRB.mass = 1;
                /*Destroy(tileRB);
                towerRB.bodyType = RigidbodyType2D.Kinematic;
                transform.parent = GameObject.Find("Tower").transform;
                towerRB.velocity = Vector2.zero;
                towerRB.bodyType = RigidbodyType2D.Dynamic;*/
                done = true;
            }
        }
        else if (Globals.gameOver)
        {
            tileRB.velocity = new Vector2(0, 0);
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
