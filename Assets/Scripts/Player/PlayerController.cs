using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Jump System")]
    [SerializeField] float jumpTime;
    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;
    public GameObject menuGameOver;

    [Header("Collision")]
    public float groundLength = 1.2f;
    public LayerMask groundLayer;

    //Private variables
    int playerHealth;
    Rigidbody2D rb;
    bool isGrounded;
    bool isJumping;
    Vector2 vecGravity;
    float jumpCounter;
    public bool isAlive;
    public int numPowerUp = 0;


    void Start()
    {

        isAlive = true;

        playerHealth = 2;

        //Variable of the player gravity
        vecGravity = new Vector2(0, -Physics2D.gravity.y);

        //Access the player Rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Check if player is on ground 
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundLayer);

        //Jump Logic
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && isAlive)
        {
            JumpImpulse();
            isJumping = true;
            jumpCounter = 0;
        }
        
        //Jump multiplier
        if(rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            rb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }

        //Gravity multiplier
        if(rb.velocity.y <0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if(playerHealth <= 0)
        {
            isAlive = false;
            menuGameOver.SetActive(true);
        }

    }

    //On collision activate jump mechanic and destroy obstacle gameObject
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            playerHealth -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WeakPoint"))
        {
            JumpImpulse();
            numPowerUp++;
        }

        if (collision.gameObject.CompareTag("HardObstacle"))
        {
            Destroy(collision.gameObject);
            playerHealth -= 2;
        }
    }

    //Jump mechanic
    void JumpImpulse()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    //Draw raycast that know if player is on the ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }

    
    public void RestartCurrentScene()
    {
        // Obtiene el nombre de la escena actual y la recarga
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    
}
