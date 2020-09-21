using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eenemy5 : MonoBehaviour
{

    float timer;
    public int pointsDropped;
    public float health;
    float savedHealth;
    public float shotsPerSecond;
    public float projectileSpeed;
    public float movementSpeed;
    public float chargeBarValue;
    public GameObject Drop1;
    public GameObject TripleShot;
    GameObject explosionPool;
    ObjectPooler explosionPooler;

    private void Start()
    {
        explosionPool = GameObject.FindGameObjectWithTag("ExplosionPool");
        explosionPooler = explosionPool.GetComponent<ObjectPooler>();
        savedHealth = health;
    }
    private void Fire()
    {

    }

    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * movementSpeed);
        timer += Time.deltaTime;
        if (timer > shotsPerSecond)
        {
            Fire();
            timer = 0;
        }
        if (isOffScreen() || health <= 0)
        {
            Slider test = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();

            if (health <= 0)
            {
                FindObjectOfType<Movement>().IncreasePoints(pointsDropped);
                test.value += chargeBarValue;
                explosionPooler.GetPooledObject(gameObject.transform.position, transform.rotation).transform.localScale += new Vector3(0, 0);
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
            Destroy();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            Movement player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
            ReceiveDamage(player.damage);
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
            ReceiveDamage(health);

        else if (collision.gameObject.tag == "SpecialMove")
            ReceiveDamage(health);
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
