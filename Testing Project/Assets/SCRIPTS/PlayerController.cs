using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float kecepatan, gayaLompat, checkRadius, kecepatanDorong, waktuMulaiDorong;
    private float moveInput,waktuDorong;

    private Rigidbody2D rb2d;

    private bool facingRight = true;
    private bool isGround, isgrnd, isTouch;
    private bool wallSlide,wallJumping;
    public float  wallSlidingSpeed,x_wallJump,y_wallJump,wallJumpTime;
    private bool isJump;
    public Transform groundCheck,frontCheck;
    public LayerMask whatIsGround,whatIsWall;

    private int lompat, arahDorong;
    public int nilaiLompat;

    private float penghitungWaktuLompat;
    public float waktuLompatan;

    private void Start()
    {
        lompat = nilaiLompat;
        rb2d = GetComponent<Rigidbody2D>();
        waktuDorong = waktuMulaiDorong;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2 (moveInput * kecepatan, rb2d.velocity.y);

        //doublejump
        if(isGround == true){
            lompat = nilaiLompat;
        } else if(isgrnd == true){
            lompat = nilaiLompat;
        }

        if(Input.GetKeyDown(KeyCode.Space) && lompat > 0){
            rb2d.velocity = Vector2.up * gayaLompat;
            lompat = lompat - 1;
            isJump = true;
            penghitungWaktuLompat = waktuLompatan;
        }else if (Input.GetKeyDown(KeyCode.Space) && lompat == 0 && isGround == true){
            rb2d.velocity = Vector2.up * gayaLompat;
        } else if(Input.GetKeyDown(KeyCode.Space) && lompat == 0 && isgrnd == true){
            rb2d.velocity = Vector2.up * gayaLompat;
        }

        //Holdjump 
        if(Input.GetKey(KeyCode.Space) && isJump == true ){
            if(penghitungWaktuLompat > 0){
                rb2d.velocity = Vector2.up * gayaLompat;
                penghitungWaktuLompat -= Time.deltaTime;
            } else {
                isJump = false;
            }
        } else if (Input.GetKeyUp(KeyCode.Space)){
            isJump = false;
        }

        //DASHING
        if (arahDorong == 0){
            if(Input.GetKeyDown(KeyCode.F)){
                if (moveInput > 0){
                    Debug.Log("Kanan");
                    arahDorong = 1;
                } else if (moveInput < 0){
                    Debug.Log("Kiri");
                    arahDorong = 2;
                }
            }
        } else {
            if(waktuDorong <= 0){
                arahDorong = 0;
                waktuDorong = waktuMulaiDorong;
                rb2d.velocity = Vector2.zero;
            } else {
                waktuDorong -= Time.deltaTime;
                if (arahDorong == 1){
                    rb2d.velocity = Vector2.right * kecepatanDorong;
                } else if (arahDorong == 2){
                    rb2d.velocity = Vector2.left * kecepatanDorong;
                }
            }
        } 


        //walljump
        if (isTouch == true && isGround == false & moveInput != 0){
            wallSlide = true;
        } else {
            wallSlide = false;
        }
        if(wallSlide){
            rb2d.velocity = new Vector2(rb2d.velocity.x ,Mathf.Clamp(rb2d.velocity.y , -wallSlidingSpeed, float.MaxValue));
        }
        if(Input.GetKeyDown(KeyCode.Space) && wallSlide == true){
            wallJumping = true;
            Invoke("SetJump",wallJumpTime);
        }
        if(wallJumping == true){
            rb2d.velocity = new Vector2(x_wallJump * -moveInput, y_wallJump);
        }

    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouch  = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsWall);
        isgrnd = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsWall);

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

    void SetJump(){
        wallJumping = false;
    }
}
