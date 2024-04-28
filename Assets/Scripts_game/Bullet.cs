using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatisSolid;

    [SerializeField] bool enemyBullet;

    public GameObject bulleteffect;

    private void Start()
    {
        Invoke("DestroyBullet", lifetime);
    }

    private void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, distance, whatisSolid);
        if(hitinfo.collider != null)
        {
            if (hitinfo.collider.CompareTag("Enemy"))
            {
                hitinfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (hitinfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitinfo.collider.GetComponent<Player>().ChangeHealth(-damage);
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    public void DestroyBullet()
    {
            Instantiate(bulleteffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
    }
}
