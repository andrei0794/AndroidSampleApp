﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForceUp, jumpForceDown;

    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerBC;

    private bool isGrounded;
    private float jumpPos;
    private float lastForceUsed;

	// Use this for initialization
	void Start () {

        playerRigidBody = GetComponent<Rigidbody2D>();
        playerBC = GetComponent<BoxCollider2D>();

        isGrounded = true;
        jumpForceUp = 0.005f;
    }
	
	// Update is called once per frame
	void Update () {
        if (isGrounded && Input.GetMouseButtonDown(0))
        {
            jumpPos = playerRigidBody.position.y;
            playerRigidBody.AddForce(new Vector2(0, jumpForceUp), ForceMode2D.Impulse);
            lastForceUsed = jumpForceUp;     
        }
        else if (!isGrounded && playerRigidBody.position.y > jumpPos + 1.5f)
        {
            jumpForceDown = lastForceUsed / 2;
            playerRigidBody.AddForce(new Vector2(0, -jumpForceDown), ForceMode2D.Impulse);
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
                Globals.GetInstance()._gameOver = true;
                playerRigidBody.constraints = RigidbodyConstraints2D.None;
                playerRigidBody.AddForce(new Vector2(Globals.PLAYER_PUSH_FORCE_X, Globals.PLAYER_PUSH_FORCE_Y), 
                    ForceMode2D.Impulse);        
            }
            else if (Vector3.Dot(contact.normal, Vector2.left) > 0.5) //actually right
            {
                Globals.GetInstance()._gameOver = true;
                playerRigidBody.constraints = RigidbodyConstraints2D.None;
                playerRigidBody.AddForce(new Vector2(-Globals.PLAYER_PUSH_FORCE_X, Globals.PLAYER_PUSH_FORCE_Y), 
                    ForceMode2D.Impulse);          
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
