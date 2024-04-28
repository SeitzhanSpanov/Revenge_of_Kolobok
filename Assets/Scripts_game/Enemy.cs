using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwattack;

    public GameObject floatingdamage;
    public float startTimeBtwattack;
    public int health;
    public float speed;
    public GameObject deatheffect;
    public int damage;
    private float stopTime;
    public float startstoptime;
    public float normalSpeed;
    private Player player;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
 
    }

    private void Update()
    {
        if(stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            Instantiate(deatheffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 360, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,speed*Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        stopTime = startstoptime;
        health -= damage;
        Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 2.75f);
        Instantiate(floatingdamage, damagePos, Quaternion.identity);
        floatingdamage.GetComponentInChildren<floatingdamage>().damage = damage;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(timeBtwattack <= 0)
            {
                anim.SetTrigger("enemyattack");
            }
            else
            {
                timeBtwattack -= Time.deltaTime;
            }
        }
    }
    public void onEnemyattack()
    {
        player.ChangeHealth(-damage);
        timeBtwattack = startTimeBtwattack;
    }



}
