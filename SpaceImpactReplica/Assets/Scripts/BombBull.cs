using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBull : MonoBehaviour
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
        if (gameObject.tag == "BombBull")
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
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
