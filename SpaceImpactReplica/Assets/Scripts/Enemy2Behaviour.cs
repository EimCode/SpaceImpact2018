using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Behaviour : MonoBehaviour {

    
    public int pointsDropped;
    public float health;
    float savedHealth;
    public float movementSpeed;
    public float chargeBarValue;
    public float damage;
    GameObject player;
    float yDifferenceGoal;
    GameObject explosionPool;
    ObjectPooler explosionPooler;
    public GameObject Drop1;
    public GameObject TripleShot;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        yDifferenceGoal = 0 - transform.position.y;
        explosionPool = GameObject.FindGameObjectWithTag("ExplosionPool");
        explosionPooler = explosionPool.GetComponent<ObjectPooler>();
        savedHealth = health;
    }

    private void Update()
    {
        if (player.Equals(null))
            return;
        int yDir;
        if (player.transform.position.y - transform.position.y < yDifferenceGoal-1)
            yDir = 1;
        else if (player.transform.position.y - transform.position.y > yDifferenceGoal+1)
            yDir = -1;
        else
            yDir = 0;
        transform.Translate(new Vector2(yDir,-1) * Time.deltaTime * movementSpeed);
        float newY = Mathf.Clamp(transform.position.y, -4.3f, 4.3f);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.position.z);

        if (isOffScreen())
        {
            Destroy();
        }        
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
        else if (collision.gameObject.tag == "LaserBull")
        {
            LaserBull missile = collision.gameObject.GetComponent<LaserBull>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            Destroy();
        }
        else if (collision.gameObject.tag == "RocketBull")
        {
            RocketBull missile = collision.gameObject.GetComponent<RocketBull>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            ReceiveDamage(player.damage);
            missile.Hit();
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
            explosionPooler.GetPooledObject(transform.position, transform.rotation);
            Destroy();
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
        }

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
