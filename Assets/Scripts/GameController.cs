using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject tile;
    public GameObject lastTile;
    public GameObject lastStackedTile;

    private TileController lastTileController;

    private BoxCollider2D lastTileBoxCollider;
    private Rigidbody2D lastTileRigidBody;
    private GameObject player;

    // Use this for initialization
    void Start () {

        Globals.GetInstance()._gameOver = false;
        Globals.GetInstance()._tileCounter = 0;

        /*Calculating camera view coordinates*/
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        Globals.GetInstance()._minX = bottomCorner.x;
        Globals.GetInstance()._maxX = topCorner.x;
        Globals.GetInstance()._minY = bottomCorner.y;
        Globals.GetInstance()._maxY = topCorner.y;
        
        /*Getting necessary components*/
        lastTileBoxCollider = lastTile.GetComponent<BoxCollider2D>();
        lastTileRigidBody = lastTile.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        /*Spawning the first tile*/
        Vector2 spawnPosition = new Vector2(Globals.GetInstance()._minX, lastTile.transform.position.y + lastTileBoxCollider.bounds.size.y + 0.03f);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
        lastTile = Instantiate(tile, spawnPosition, spawnRotation);
        lastTile.GetComponent<TileController>().direction = TileDirection._left;
        Globals.GetInstance()._tileCounter++;
    }

    // Update is called once per frame
    void Update() {

        lastTileController = lastTile.GetComponent<TileController>();
        Vector2 spawnPosition;
        TileDirection directionLocal;

        if (lastTileController.done == true && !Globals.GetInstance()._gameOver)
        {
            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                spawnPosition = new Vector2(Globals.GetInstance()._minX, lastTile.transform.position.y + lastTileBoxCollider.bounds.size.y + 0.03f);
                directionLocal = TileDirection._left;
            }
            else
            {
                spawnPosition = new Vector2(Globals.GetInstance()._maxX, lastTile.transform.position.y + lastTileBoxCollider.bounds.size.y + 0.03f);
                directionLocal = TileDirection._right;
            }
            Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
            lastTile = Instantiate(tile, spawnPosition, spawnRotation);
            lastTile.GetComponent<TileController>().direction = directionLocal;
        }

        if (Globals.GetInstance()._gameOver)
        {
            if (player.transform.position.y < lastTile.transform.position.y - lastTileBoxCollider.bounds.size.y)
            {
                UnityEngine.Advertisements.Advertisement.Show();
                SceneManager.LoadScene(0);
            }
        }
    }
}
