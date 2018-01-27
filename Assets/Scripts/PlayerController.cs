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
            _cata.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts[0].point;
        float offset = contactPoint.y - (_cataBC.transform.position.y + _cataBC.bounds.size.y / 2);
        bool bottom =  offset < 0.1 || offset < -0.1 ;

        if (collision.gameObject.layer == 8 && bottom)
        {
            isGrounded = true;
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
