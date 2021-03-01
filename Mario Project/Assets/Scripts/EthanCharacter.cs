using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EthanCharacter : MonoBehaviour
{
  private Animator animator;
  public Rigidbody rb;
  public LevelParserStarter level;
  public float modifier = 1;
  public float jumpForce = 1;
  [Range(-2, 2)] public float speed = 0;
  private bool jump = false;
  private bool gameOver = false;

  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  void Update()
  {
    float horizontal = Input.GetAxis("Horizontal");
    jump = (Input.GetKeyDown(KeyCode.Space)) ? true : false;

    //Set character rotation
    float y = (horizontal < 0) ? 180 : 0;
    Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);

    transform.rotation = newRotation;

    //Set character animation
    animator.SetFloat("Speed", Mathf.Abs(horizontal));

    //move character
    transform.Translate(transform.right * horizontal * modifier * Time.deltaTime);
    
  }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Brick")
        {
            Destroy(collision.collider.gameObject);
            level.GetComponent<LevelParserStarter>().increasePointCount();
        }
        if (collision.collider.gameObject.tag == "QuestionBox")
        {
            Destroy(collision.collider.gameObject);
            level.GetComponent<LevelParserStarter>().increaseCoinCount();
        }
        if(collision.collider.gameObject.tag == "Water")
        {
            Camera.main.transform.parent = null;
            Destroy(this.gameObject);
            gameOver = true;
            gameOverFunction();
            gameOver = false;
        }
        if (collision.collider.gameObject.tag == "Lava")
        {
            Camera.main.transform.parent = null;
            Destroy(this.gameObject);
            gameOver = true;
            gameOverFunction();
            gameOver = false;
        }
    }

    private void gameOverFunction()
    {
        if (gameOver)
        {
            Debug.Log("Player has Died");
        }
    }

    void FixedUpdate()
  {
    if (jump) rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        jumpForce = 4;
  }
}
