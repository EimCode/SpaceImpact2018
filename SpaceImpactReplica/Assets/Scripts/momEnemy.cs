using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class momEnemy : MonoBehaviour {

    float timer;
    public int pointsDropped = 1000;
    public float health = 500f;
    float savedHealth;
    public float firingRate = 0.5f;
    public float shotsPerSecond = 2f;
    public float projectileSpeed = 5f;
    public float movementSpeed = 2f;
    public float chargeBarValue = 10;
    GameObject player;
    public GameObject minion;
    public GameObject explosion;
    public GameObject Drop1;
    public GameObject TripleShot;
    //GameObject bulletPool;
    //ObjectPooler bulletPooler;

    private void Start()
    {
        //bulletPool = GameObject.FindGameObjectWithTag("MinionPool");
        //bulletPooler = bulletPool.GetComponent<ObjectPooler>();
        savedHealth = health;
    }
    private void Fire()
    {
        //bulletPooler.GetPooledObject(gameObject.transform.position).transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
    }

    private void Update() {
        transform.Translate(Vector2.down * Time.deltaTime * movementSpeed);
        timer += Time.deltaTime;
        if (timer > shotsPerSecond) {
            Fire();
            timer = 0;
        }
        if (isOffScreen() || health <= 0) {
            Slider test = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();            
            
            if (health <= 0)
            {
                FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
                test.value += chargeBarValue;
                Instantiate(explosion, transform.position, transform.rotation).transform.localScale += new Vector3(4, 4, 4);
                Instantiate(minion, transform.position, transform.rotation).transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                if (Random.Range(1f, 100f) <= 15f)
                {
                    Vector3 position = transform.position + new Vector3(0f, -0.8f);
                    Instantiate(TripleShot, position, TripleShot.transform.rotation);
                }
                if (Random.Range(1f, 100f) >= 95f)
                {
                    Vector3 position = transform.position + new Vector3(0f, -0.8f);
                    Instantiate(TripleShot, position, transform.rotation);
                }
            }
            Destroy();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "PlayerProjectile") {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            player = GameObject.FindWithTag("Player");
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
            ReceiveDamage(player.damage);
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
            ReceiveDamage(player.damage);
            missile.Hit();
        }
        else if (collision.gameObject.tag == "Player")
            ReceiveDamage(health);

        else if (collision.gameObject.tag == "SpecialMove") 
            ReceiveDamage(health);
        
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        
    }
    bool isOffScreen()
    {
        if (transform.position.x < -9 || Mathf.Abs(transform.position.y) > 5)
            return true;
        return false;
    }
    void Destroy()
    {
        gameObject.SetActive(false);
        health = savedHealth;
    }
}
