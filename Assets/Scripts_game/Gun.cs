using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Guntype guntype;
    public float offset;
    public GameObject bullet;
    public Joystick joystick;
    public Transform shortpoint;
    
    public enum Guntype {Default, Enemy}

    private float timeBtwShort;
    public float starttimeBtwShort;
    private float rotZ;
    private Vector3 difference;
    private Player player;
    
    
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.controltype == Player.ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if(guntype == Guntype.Default)
        {
        if (player.controltype == Player.ControlType.PC)
        {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else if(player.controltype == Player.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
        {
            rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        } 
        }
        else if(guntype == Guntype.Enemy)
        {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
  
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (timeBtwShort <= 0)
        {
            if (Input.GetMouseButton(0)&& player.controltype == Player.ControlType.PC || guntype == Guntype.Enemy)
            {
                Shoot();
            }
            else if (player.controltype == Player.ControlType.Android)
            {
                if(joystick.Horizontal !=0 || joystick.Vertical != 0)
                {
                    Shoot();
                }
            }
        }
        else
        {
            timeBtwShort -= Time.deltaTime;
        }
    }
    public void Shoot()
    {
                Instantiate(bullet, shortpoint.position, shortpoint.rotation);
                timeBtwShort = starttimeBtwShort;
    }
}
