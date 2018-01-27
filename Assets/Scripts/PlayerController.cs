using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _cata;
    private BoxCollider2D _cataBox;

    public bool isGrounded;

	// Use this for initialization
	void Start () {

        _cata = GetComponent<Rigidbody2D>();
        _cataBox = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isGrounded && Input.GetMouseButtonDown(0))
        {
            _cata.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 && !isGrounded)
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8 && isGrounded)
        {
            isGrounded = false;
        }
    }
}
