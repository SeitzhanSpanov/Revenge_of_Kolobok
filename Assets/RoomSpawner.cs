using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{ public Direction direction;
 public enum Direction
    {
        Top,
        Left,
        Right,
        Bottom,
        None,
    }
    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }
    public void Spawn()
    {
        if (!spawned)
        {
            if(direction == Direction.Top)
            {
                rand = Random.Range(0, variants.toprooms.Length);
                Instantiate(variants.toprooms[rand], transform.position, variants.toprooms[rand].transform.rotation);
            }
            else if (direction == Direction.Bottom)
            {
                rand = Random.Range(0, variants.bottomrooms.Length);
                Instantiate(variants.bottomrooms[rand], transform.position, variants.bottomrooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.rightrooms.Length);
                Instantiate(variants.rightrooms[rand], transform.position, variants.rightrooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.leftrooms.Length);
                Instantiate(variants.leftrooms[rand], transform.position, variants.leftrooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("RoomPoint")&& other.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
} 
