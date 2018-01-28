using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _cata;
    private BoxCollider2D _cataBC;

    private bool isGrounded;

	// Use this for initialization
	void Start () {

        _cata = GetComponent<Rigidbody2D>();
        _cataBC = GetComponent<BoxCollider2D>();

        isGrounded = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (isGrounded && Input.GetMouseButtonDown(0))
        {
            _cata.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts[0].point;
        float offset = contactPoint.y - (_cataBC.transform.position.y + _cataBC.bounds.size.y / 2);
        bool bottom =  offset < 0.1 || offset < -0.1 ;
        /*
        offset = contactPoint.x - (_cataBC.transform.position.x - _cataBC.bounds.size.x / 2);
        bool left = offset < 0.1 || offset < -0.1;
        offset = contactPoint.x - (_cataBC.transform.position.x + _cataBC.bounds.size.x / 2);
        bool right = offset < 0.1 || offset < -0.1;
        */
        if (collision.gameObject.layer == 8 && bottom)
        {
            isGrounded = true;
        }
        /*
        else if (collision.gameObject.layer == 8 && left)
        {
            _cata.AddForce(new Vector2(3, 0.5f), ForceMode2D.Impulse);
        }

        else if (collision.gameObject.layer == 8 && right)
        {
            _cata.AddForce(new Vector2(-3, 0.5f), ForceMode2D.Impulse);
        }
        */
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && isGrounded)
        {
            isGrounded = false;
        }
    }
}
