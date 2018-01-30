using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;

    private bool isGrounded;

	// Use this for initialization
	void Start () {

        playerRB = GetComponent<Rigidbody2D>();
        playerBC = GetComponent<BoxCollider2D>();

        isGrounded = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (isGrounded && Input.GetMouseButtonDown(0))
        {
            playerRB.AddForce(new Vector2(0, 0.0006f), ForceMode2D.Impulse);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0 && collision.gameObject.layer == 8)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector2.up) > 0.5)
            {
                isGrounded = true;
            }
            else if (Vector3.Dot(contact.normal, Vector2.right) > 0.5) //actually left
            {
                // Vector3 dir = Quaternion.AngleAxis(45, Vector3.forward) * Vector3.right;
                //playerRB.AddForce(dir * 10);
                playerRB.constraints = RigidbodyConstraints2D.None;
                playerRB.AddForce(new Vector2(2,1), ForceMode2D.Impulse);
                print("right");
                Globals.gameOver = true;
            }
            else if (Vector3.Dot(contact.normal, Vector2.left) > 0.5) //actually right
            {
                playerRB.constraints = RigidbodyConstraints2D.None;
                playerRB.AddForce(new Vector2(-2,1), ForceMode2D.Impulse);
                Globals.gameOver = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && isGrounded)
        {
            isGrounded = false;
        }
    }
}
