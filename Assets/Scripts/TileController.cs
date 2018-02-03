using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileController : MonoBehaviour {

    public bool done;
    private int last_count;

    public TileDirection direction; 

    private GameObject floor;
    private GameObject tower;
    private GameObject player;

    private BoxCollider2D floorBoxCollider;
    private BoxCollider2D tileBoxCollider;

    private Rigidbody2D towerRigidBody;
    private Rigidbody2D tileRigidBody;

    private GameController gameController;

    private Vector3 destination;

    // Use this for initialization
    void Start () {
        floor = GameObject.Find("Floor");
        floorBoxCollider = floor.GetComponent<BoxCollider2D>();

        player = GameObject.Find("Player");
        done = false;

        tileRigidBody = GetComponent<Rigidbody2D>();
        tileBoxCollider = GetComponent<BoxCollider2D>();

        if (Globals.GetInstance()._tileCounter == 0)
        {
            destination = new Vector3(floor.transform.position.x, floor.transform.position.y , 0);
        }
        else
        {
            BoxCollider2D lastTileBoxCollider = Globals.GetInstance()._lastTile.GetComponent<BoxCollider2D>();
            destination = new Vector3(floor.transform.position.x, Globals.GetInstance()._lastTile.transform.position.y , 0);
        }

        last_count = 0;

        tower = GameObject.Find("Tower");
        towerRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Globals.GetInstance()._tileCounter > last_count + Globals.TILE_NUMBER_DIFFICULTY_INCREMENT)
        {
            last_count = Globals.GetInstance()._tileCounter;
            Globals.GetInstance()._tileCurrentSpeed = Globals.GetInstance()._tileCurrentSpeed + Globals.GetInstance()._tileSpeedIncrement;
            player.GetComponent<PlayerController>().jumpForceUp += Globals.GetInstance()._playerJumpForceIncrement; 
        }

        if (done == false && !Globals.GetInstance()._gameOver)
        {
            if (direction == TileDirection._left)
            {
                tileRigidBody.velocity = transform.right * UnityEngine.Random.Range(Globals.GetInstance()._tileCurrentSpeed - Globals.TILE_SPEED_VARIATION,
                    Globals.GetInstance()._tileCurrentSpeed + Globals.TILE_SPEED_VARIATION);   
            }
            else if (direction == TileDirection._right)
            {
                tileRigidBody.velocity = - transform.right * UnityEngine.Random.Range(Globals.GetInstance()._tileCurrentSpeed - Globals.TILE_SPEED_VARIATION,
                    Globals.GetInstance()._tileCurrentSpeed + Globals.TILE_SPEED_VARIATION);
            }
            if ( tileBoxCollider.IsTouching(player.GetComponent<BoxCollider2D>()))
            {
                tileRigidBody.velocity = new Vector2(0, -Globals.TILE_FALL_VELOCITY);
                //tileRigidBody.angularVelocity = 0f;
                //tileRigidBody.bodyType = RigidbodyType2D.Dynamic;
                //tileRigidBody.velocity = Vector2.zero;
                //tileRigidBody.angularVelocity = 0f;
                //tileRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                //tileRigidBody.gravityScale = 1;
                //tileRigidBody.mass = 1;
                /*Destroy(tileRigidBody);
                towerRigidBody.bodyType = RigidbodyType2D.Kinematic;
                transform.parent = GameObject.Find("Tower").transform;
                towerRigidBody.velocity = Vector2.zero;
                towerRigidBody.bodyType = RigidbodyType2D.Dynamic;*/
                done = true;

                Globals.GetInstance()._tileCounter++; 
            }
        }
        else if (Globals.GetInstance()._gameOver)
        {
            tileRigidBody.velocity = new Vector2(0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0 && collision.gameObject.tag == "Tile")
        {
            if (Math.Abs(tileRigidBody.transform.position.x - 
                Globals.GetInstance()._lastTile.GetComponent<Rigidbody2D>().transform.position.x) <
                Globals.GetInstance()._lastTile.GetComponent<BoxCollider2D>().bounds.size.x)
            {
                tileRigidBody.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
