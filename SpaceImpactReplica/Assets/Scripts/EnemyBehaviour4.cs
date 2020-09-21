using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour4 : MonoBehaviour {

    
    public int pointsDropped;
    public float health;
    float savedHealth;
    public float movementSpeed;
    public float chargeBarValue;
    public float damage;
    GameObject player;
    public GameObject Drop1;
    public GameObject TripleShot;
    float timer;
    public float secondsPerShot;
    int yDir;
    int xDir = -5;
    GameObject bulletPool;
    ObjectPooler bulletPooler;
    GameObject explosionPool;
    ObjectPooler explosionPooler;


    private void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("EnemyBulletPool1");
        bulletPooler = bulletPool.GetComponent<ObjectPooler>();
        explosionPool = GameObject.FindGameObjectWithTag("ExplosionPool");
        explosionPooler = explosionPool.GetComponent<ObjectPooler>();
        player = GameObject.FindWithTag("Player");
        savedHealth = health;
    }

    private void Update()
    {
        GameObject.FindGameObjectWithTag("GameControl").GetComponent<EnemySpawn>().SpawnBoss = false;
        if (player.Equals(null))
            return;

        if (player.Equals(null))
            return;

        if (transform.position.x > 7)
            xDir = -5;
        else if (transform.position.x < -10)
            xDir = 1;

        if (player.transform.position.y > transform.position.y +0.6f)
            yDir = 1;
        else if (player.transform.position.y < transform.position.y -0.6f)
            yDir = -1;
        else
            yDir = 0;

        timer += Time.deltaTime;
        if (timer > secondsPerShot && xDir == 1)
        {
            Fire();
            timer = 0;
        }

        transform.Translate(new Vector2(xDir, yDir) * Time.deltaTime * movementSpeed);
        float newY = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.position.z);

        if (isOffScreen())
        {
            Destroy(gameObject);
        }        
    }
    private void Fire()
    {
        Vector3 position = transform.position + new Vector3(-2f, 0f);
        GameObject missile = bulletPooler.GetPooledObject(position);
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(-8f, 0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            ReceiveDamage(player.GetComponent<Movement>().damage);
            missile.Hit();
        }
        else if (collision.gameObject.tag == "PlasmaBull")
        {
            PlasmaBull missile = collision.gameObject.GetComponent<PlasmaBull>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            ReceiveDamage(player.damage);
            missile.Hit();
        }
        else if (collision.gameObject.tag == "FlatBull")
        {
            FlatBull missile = collision.gameObject.GetComponent<FlatBull>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            ReceiveDamage(player.damage);
            missile.Hit();
        }
        else if (collision.gameObject.tag == "DefensiveBull")
        {
            DefensiveBull missile = collision.gameObject.GetComponent<DefensiveBull>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            Destroy();
            missile.Hit();
        }
        else if (collision.gameObject.tag == "RocketBull")
        {
            RocketBull missile = collision.gameObject.GetComponent<RocketBull>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            ReceiveDamage(player.damage);
            missile.Hit();
        }
        else if (collision.gameObject.tag == "LaserBull")
        {
            LaserBull missile = collision.gameObject.GetComponent<LaserBull>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            Destroy();
        }
        else if (collision.gameObject.tag == "Player")
        {
            ReceiveDamage(health);
        }
        else if (collision.gameObject.tag == "SpecialMove")
        {
            ReceiveDamage(health);
        }
        if (health <= 0)
        {
            FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
            Slider test = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();
            test.value += chargeBarValue;
            explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(2,2);
            if (Random.Range(1f, 100f) <= 20f)
            {
                Vector3 position = transform.position + new Vector3(0f, -0.8f);
                Instantiate(Drop1, position, transform.rotation);
            }
            if (Random.Range(1f, 100f) > 95f)
            {
                Vector3 position = transform.position + new Vector3(0f, -0.8f);
                Instantiate(TripleShot, position, TripleShot.transform.rotation);
            }
            GameObject.FindGameObjectWithTag("GameControl").GetComponent<EnemySpawn>().SpawnEnemies = true;
            Destroy(gameObject);
        }

    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;

    }
    bool isOffScreen()
    {
        if (transform.position.x < -11 || Mathf.Abs(transform.position.y) > 5)
            return true;
        return false;
    }
    void Destroy()
    {
        gameObject.SetActive(false);
        health = savedHealth;
    }
}
