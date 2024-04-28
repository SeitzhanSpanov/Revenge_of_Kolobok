using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum ControlType {PC, Android};
    public ControlType controltype;
    public Joystick joystick;
    public float speed;

    public Text healthDisplay;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;
    private bool facingLeft =true;
    public GameObject cheeseeffect;
    public GameObject shield;
    public Shield shieldTimer;
    public int health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controltype == ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if(controltype == ControlType.Android)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        moveVelocity = moveInput.normalized * speed;
        if(moveInput.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
        if (!facingLeft && moveInput.x > 0)
        {
            Flip();
        }
       else if(facingLeft && moveInput.x < 0)
        {
            Flip();
        }
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cheese"))
        {
            ChangeHealth(5);
            Instantiate(cheeseeffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            if (!shield.activeInHierarchy)
            {
            shield.SetActive(true);
            shieldTimer.gameObject.SetActive(true);
            shieldTimer.isCooldown = true;
            Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResetTimer();
                Destroy(other.gameObject);
            }

        }
    }
    public void ChangeHealth(int healthvalue)
    {
        if(!shield.activeInHierarchy || shield.activeInHierarchy && healthvalue > 0)
        {
        health += healthvalue;
        healthDisplay.text = "HP: " + health;
        }
        else if(shield.activeInHierarchy && healthvalue < 0)
        {
            shieldTimer.ReduceTime(healthvalue);
        }
        
    }
}
