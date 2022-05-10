using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    // slowdown the movement by setting our own speed
    public float speed = 2f;
    public float playerVelocity = 6f;

    //access to the Players RG properties
    public Rigidbody2D PlayerRB;

    public Animator PlayerANI;

    public float score;
    public TextMeshProUGUI DisplayScore;
    public float lives = 3;
    public TextMeshProUGUI DisplayLives;

    public Vector2 ReturnPoint;
    public GameObject Point2;

    public bool isOnGround;

    // Sound FX
    public AudioSource coinSnd;
    public AudioSource bombSnd;
    public AudioSource gameSnd;
    public bool soundStatus = true;


    public Button soundBtn;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;


    // Start is called before the first frame update
    void Start()
    {
        ReturnPoint = transform.position;
        print(ReturnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        playerJump();
        playerScale();
        updateScore();
        print(isOnGround);
    }

    // player movement function
    void playerMovement()
    {
        // playermovement
        // get access the Axis - Hoz and Vertical
        float hInput = Input.GetAxis("Horizontal");
        //float vInput = Input.GetAxis("Vertical");
        // find the direction and set a new position
        Vector2 playerDirection = new Vector2(hInput, 0);
        // smooth movement - tranform.translate
        transform.Translate(playerDirection * speed * Time.deltaTime);

        //direction
        //Animation Controller
        if (playerDirection != Vector2.zero)
        {
            print("you are moving");
            PlayerANI.SetBool("IsMoving", true);
        }
        else
        {
            print("you are still");
            PlayerANI.SetBool("IsMoving", false);
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    void playerJump()
    {
        // if they have pressed space then send the player UP UP and AWAY
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            print("you are hitting the space bar");
            PlayerRB.velocity = Vector2.up * playerVelocity;
            PlayerANI.SetBool("IsJumping", true);
        }
    }

    void playerScale()
    {
        // veriable to hold the x, y and z scale
        Vector2 newScale = transform.localScale;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            newScale.x = -0.4f;
            CameraFollow.offSetX = -4;

            //updating the character
            print(newScale);
        }
        // right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            newScale.x = 0.4f;
            CameraFollow.offSetX = 4;
            // updating the character
        }
        // update the scale
        transform.localScale = newScale;

    }

    void updateScore()
    {
        DisplayScore.text = score.ToString();
        DisplayLives.text = lives.ToString();
    }

    // *****UI Button*****
    public void soundControl()
    {
        print("you are clicking on the sound button");
        gameSnd.Stop();
        if (soundStatus)
        {
            gameSnd.Stop();
            soundStatus = false;
            soundBtn.image.sprite = soundOffIcon;
        }
        else
        {
            gameSnd.Play();
            soundStatus = true;
            soundBtn.image.sprite = soundOnIcon;
        }
    }

    // *********COLLISION AREA **********

    // sense the collision between the player and the ground
    // control back to idle animation
    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("you are on the ground");
        if (collision.gameObject.tag == "Ground")
        {
            PlayerANI.SetBool("IsJumping", false);
            isOnGround = true;
        }
    }

    // set up exit
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = false;
        }
    }



    //trigger eventhadler
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            score += 1;
            if (soundStatus)
            {
                coinSnd.Play();
            }
            coinSnd.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            // reduce life
            lives -= 1;
            bombSnd.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "EndPoint")
        {
            print("The end of the level");
            SceneManager.LoadScene("End Game");
        }
    }
}
        
        
    





