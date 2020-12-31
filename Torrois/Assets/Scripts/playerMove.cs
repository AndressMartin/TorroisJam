using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D Mybox2d;

    float horizontal;
    float vertical;

    public float runSpeed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Mybox2d = GetComponent<BoxCollider2D>();
    }

    void Update() 
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical).normalized * runSpeed;
        Debug.Log(rb.velocity);
    }
}
