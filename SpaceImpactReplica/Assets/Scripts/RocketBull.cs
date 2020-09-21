using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBull : MonoBehaviour
{
    public float damage = 100f;
    GameObject explosionPool;
    ObjectPooler explosionPooler;

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
        if (gameObject.tag == "RocketBull")
        {
            switch (collision.gameObject.tag)
            {
                case "Enemy":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Enemy2":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Enemy3":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Enemy4":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Eenemy":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Eenemy2":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Eenemy3":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Eenemy4":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Eenemy5":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                case "Boss":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                default:
                    break;
            }
        }
        else if (gameObject.tag == "EnemyProjectile")
        {
            switch (collision.gameObject.tag)
            {
                case "Player":
                    explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(-1.5f, -1.5f, 1.5f);
                    break;
                default:
                    break;
            }
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
