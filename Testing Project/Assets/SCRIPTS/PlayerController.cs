using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float kecepatan, gayaLompat, checkRadius;
    private float moveInput;

    private Rigidbody2D rb2d;

    private bool facingRight = true;
    private bool isGround;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private int lompat;
    public int nilaiLompat;

    private void Start()
    {
        lompat = nilaiLompat;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isGround == true){
            lompat = nilaiLompat;
        }

        if(Input.GetKeyDown(KeyCode.Space) && lompat > 0){
            rb2d.velocity = Vector2.up * gayaLompat;
            lompat--;
        }else if (Input.GetKeyDown(KeyCode.Space) && lompat == 0 && isGround == true){
            rb2d.velocity = Vector2.up * gayaLompat;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2 (moveInput * kecepatan, rb2d.velocity.y);

        if (facingRight == false && moveInput > 0){
            Flip();
        } else if (facingRight == true && moveInput < 0){
            Flip();
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 Skalar = transform.localScale;
        Skalar.x *= -1;
        transform.localScale = Skalar;
    }
}
