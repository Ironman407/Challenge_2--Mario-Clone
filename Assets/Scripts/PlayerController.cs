using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb2d;
    private AudioSource source;
    public AudioClip jumpClip;
    public Animator animator;

    private bool facingRight = true;
    public float speed;
    public float jumpforce;
    public Text countText;
    private int count;
    private bool isDead;
    private bool isJumping;
    public float groundCheckWidth, groundCheckHeight;
    //ground check
    private bool isOnGround;
    public GameObject groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    //audio stuff

    void Awake()
    {
         rb2d = GetComponent<Rigidbody2D>();
         source = GetComponent<AudioSource>();
         animator = GetComponent<Animator>();
         isDead = false;
         isJumping = false;
    }

    private void Update()
    {
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapBox(groundcheck.transform.position, new Vector2(groundCheckWidth, groundCheckHeight), 270, allGround);

        Debug.Log(isOnGround);



        //stuff I added to flip my character
        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {


            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.velocity = Vector2.up * jumpforce;
                // Audio stuff
                source.PlayOneShot(jumpClip);


            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag ("Pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }
}
