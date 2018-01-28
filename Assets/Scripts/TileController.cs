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

        speed = 2;
        last_count = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (gc.i > last_count + 5)
        {
            last_count = gc.i;
            speed = speed + 0.4f;
        }

        if (done == false)
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
              
                tileRB.velocity = new Vector2(0, 0);
               // tileRB.bodyType = RigidbodyType2D.Dynamic;
              //  tileRB.gravityScale = 1f;
              //  tileRB.mass = 1000000;
                //Destroy(tileRB);
                //transform.parent = GameObject.Find("Tower").transform;
                done = true;
               if (GameObject.Find("Player").transform.position.y >= Camera.main.transform.position.y)
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
                Debug.Log("TRUE\n");
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
