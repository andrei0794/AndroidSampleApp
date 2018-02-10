using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using G = Globals;

public class TileController : MonoBehaviour {
    public bool done;
    private int last_count;

    public TileDirection direction; 

    private GameObject floor;
    private GameObject tower;
    private GameObject player;

    private static int lastFreeze = 0;

    private BoxCollider2D floorBoxCollider;
    private BoxCollider2D tileBoxCollider;

    private Rigidbody2D towerRigidBody;
    private Rigidbody2D tileRigidBody;

    private GameController gameController;

    private int tile_no;
    private bool freezed;

    private static int contor = 0;

    // Use this for initialization
    void Start () {
        freezed = false;

        tile_no = Globals.GetInstance()._tileCounter;

        floor = GameObject.Find("Floor");
        floorBoxCollider = floor.GetComponent<BoxCollider2D>();

        player = GameObject.Find("Player");
        done = false;

        tileRigidBody = GetComponent<Rigidbody2D>();
        tileBoxCollider = GetComponent<BoxCollider2D>();

        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        last_count = 0;

        tower = GameObject.Find("Tower");
        towerRigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (done == false && Globals.GetInstance()._tileCounter > last_count + Globals.TILE_NUMBER_DIFFICULTY_INCREMENT)
        {
            last_count = Globals.GetInstance()._tileCounter;
            Globals.GetInstance()._tileCurrentSpeed = Globals.GetInstance()._tileCurrentSpeed + Globals.GetInstance()._tileSpeedIncrement;
            player.GetComponent<PlayerController>().jumpForceUp += Globals.GetInstance()._playerJumpForceIncrement; 
        }

        if (done == false && !Globals.GetInstance()._gameOver)
        {
            if (direction == TileDirection._left)
            {
                tileRigidBody.velocity = transform.right * Random.Range(Globals.GetInstance()._tileCurrentSpeed - Globals.TILE_SPEED_VARIATION,
                    Globals.GetInstance()._tileCurrentSpeed + Globals.TILE_SPEED_VARIATION);   
            }
            else if (direction == TileDirection._right)
            {
                tileRigidBody.velocity = - transform.right * Random.Range(Globals.GetInstance()._tileCurrentSpeed - Globals.TILE_SPEED_VARIATION,
                    G.GetInstance()._tileCurrentSpeed + G.TILE_SPEED_VARIATION);
            }
            if (tileBoxCollider.IsTouching(player.GetComponent<BoxCollider2D>()) && tileBoxCollider.IsTouching(gameController.lastStackedTile.GetComponent<BoxCollider2D>()))
            {
                gameController.lastStackedTile = gameObject;
                done = true;
                G.GetInstance()._tileCounter++;
                tileRigidBody.gravityScale = G.STACKED_TILE_GRAVITY;
            }
            if ( tileBoxCollider.IsTouching(player.GetComponent<BoxCollider2D>()) )
            {
                tileRigidBody.velocity = Vector2.zero;
                tileRigidBody.angularVelocity = 0f;
                tileRigidBody.bodyType = RigidbodyType2D.Dynamic;
                tileRigidBody.velocity = Vector2.zero;
                tileRigidBody.angularVelocity = 0f;
                tileRigidBody.gravityScale = G.INITIAL_TILE_GRAVITY;
                tileRigidBody.mass = 1;
                //tileRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                //Destroy(tileRigidBody);
                //towerRigidBody.bodyType = RigidbodyType2D.Kinematic;
                //transform.parent = GameObject.Find("Tower").transform;
                //towerRigidBody.velocity = Vector2.zero;
                //towerRigidBody.bodyType = RigidbodyType2D.Dynamic;

      
            }
        }
        else if (done == false && Globals.GetInstance()._gameOver)
        {
                tileRigidBody.velocity = new Vector2(0, 0);
        }

        if (freezed == false && done == true && G.GetInstance()._tileCounter > lastFreeze + G.GetInstance()._tileNumberFreeze
            && tile_no < G.GetInstance()._tileCounter && tile_no >= G.GetInstance()._tileCounter - (G.GetInstance()._tileNumberFreeze + lastFreeze))
        {
            contor++;
            if (contor == G.GetInstance()._tileNumberFreeze)
            {
                lastFreeze = lastFreeze + G.GetInstance()._tileNumberFreeze;
                contor = 0;
            }
                   
            freezed = true;
            tileRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            GameObject textMessage = GameObject.Find("InGameMessage");
            textMessage.GetComponent<Text>().text = "FREEZED";
            textMessage.GetComponent<Text>().enabled = true;
        } 

       /* if (freezed == false && tileRigidBody.position.y < Globals.GetInstance()._minY)
        {
            freezed = true;
            tileRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        } */
    }

}

