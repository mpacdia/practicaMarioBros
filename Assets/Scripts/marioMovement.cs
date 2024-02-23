using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marioMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public Collider2D col;

    public Vector2 localSpeed;
    public float movX;

    public float marioSpeed;
    public float jumpHeight;

    public bool wannaJump;
    public bool onGround;
    public bool isJumping;

    public List<AudioSource> randomAudio;
    public int index;
    public AudioSource currentAudio;

    public AudioSource yipi;
    public AudioSource waha;
    public AudioSource whoa;
    public AudioSource yahu;

    public bool grow;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        randomAudio = new List<AudioSource>();
        randomAudio.Add(yipi);
        randomAudio.Add(waha);
        randomAudio.Add(whoa);
        randomAudio.Add(yahu);

    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        localSpeed = Vector2.zero;
        onGround = true;
        isJumping = false;
        wannaJump = false;
        grow = false;

        marioSpeed = 5f;
        jumpHeight = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            wannaJump = true;
            index = Random.Range(0, randomAudio.Count);
            currentAudio = randomAudio[index];
            currentAudio.Play();
        }

        if (movX < 0)
        {
            spriteRenderer.flipX = true;
        }

        else if (movX > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("speedY", rb.velocity.y);
        animator.SetFloat("speedX", rb.velocity.x);


    }

    private void FixedUpdate()
    {
        localSpeed = new Vector2(movX, 0);

        rb.velocity = localSpeed * marioSpeed;

        if (wannaJump && onGround)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            wannaJump = false;
            onGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = true;
        }
    }
}
