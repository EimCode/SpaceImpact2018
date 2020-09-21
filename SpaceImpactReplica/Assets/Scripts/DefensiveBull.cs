using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveBull : MonoBehaviour
{
    public float damage = 100f;
    GameObject explosionPool;
    ObjectPooler explosionPooler;
    public float speed;

    Vector3 movement = new Vector3();
    float h;
    float v;

    private void Start()
    {
        explosionPool = GameObject.FindGameObjectWithTag("ExplosionPool");
        explosionPooler = explosionPool.GetComponent<ObjectPooler>();
    }
    private void Update()
    {
        if (isOffScreen())
            Destroy();
    }
    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            missile.damage = 0;
            missile.Hit();
        }
    }

    public void Hit()
    {
        Destroy();
    }
    bool isOffScreen()
    {
        if (transform.position.x < -9 || transform.position.x > 15)
            return true;
        return false;
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
