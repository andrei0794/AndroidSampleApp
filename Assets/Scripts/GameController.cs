using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject tile;
    public GameObject lastTile;
    private TileController lastTilePC;

    private BoxCollider2D lastTileBC;
    private Rigidbody2D lastTileRB;
    private GameObject player;

    // Use this for initialization
    void Start () {

        Globals.gameOver = false;
        Globals.tileCounter = 0;

        Globals.tileSpeed = 1.5f;

        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        Globals.minX = bottomCorner.x;
        Globals.maxX = topCorner.x;
        Globals.minY = bottomCorner.y;
        Globals.maxY = topCorner.y;

        lastTileBC = lastTile.GetComponent<BoxCollider2D>();
        lastTileRB = lastTile.GetComponent<Rigidbody2D>();
        
        Vector2 spawnPosition = new Vector2(Globals.minX, lastTile.transform.position.y + lastTileBC.bounds.size.y);
        Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
        lastTile = Instantiate(tile, spawnPosition, spawnRotation);
        lastTile.GetComponent<TileController>().direction = 1;
        Globals.tileCounter++;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {

        if ( Input.GetKey(KeyCode.Escape) )
        {
            SceneManager.LoadScene(0);
        }

        lastTilePC = lastTile.GetComponent<TileController>();
        Vector2 spawnPosition;
        int directionLocal;

        if (lastTilePC.done == true && !Globals.gameOver)
        {
            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                spawnPosition = new Vector2(Globals.minX, lastTile.transform.position.y + lastTileBC.bounds.size.y);
                directionLocal = 1;
            }
            else
            {
                spawnPosition = new Vector2(Globals.maxX, lastTile.transform.position.y + lastTileBC.bounds.size.y);
                directionLocal = 2;
            }
            Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
            lastTile = Instantiate(tile, spawnPosition, spawnRotation);
            lastTile.GetComponent<TileController>().direction = directionLocal;
            Globals.tileCounter++;
        }

        if (Globals.gameOver)
        {
            if (player.transform.position.y < lastTile.transform.position.y - lastTileBC.bounds.size.y)
                UnityEngine.Advertisements.Advertisement.Show();
        }
    }
}
