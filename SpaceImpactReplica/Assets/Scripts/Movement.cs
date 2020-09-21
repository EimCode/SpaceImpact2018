using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    bool x3Activated = false;
    bool rocketActivated = false;
    bool defenceActivated = false;
    bool defenceSpawn = false;
    bool flatactivated = false;
    float x3Timer;
    float rocketTimer;
    float defenceTimer;
    float flattimer;
    GameObject bulletPool;
    ObjectPooler bulletPooler;

    GameObject PlasmaBullPool;
    ObjectPooler PlasmaBullPooler;
    public GameObject PlasmaBull;

    GameObject FlatBullPool;
    ObjectPooler FlatBullPooler;
    public GameObject FlatBull;

    GameObject LaserBullPool;
    ObjectPooler LaserBullPooler;
    public GameObject LaserBull;

    GameObject RocketBullPool;
    ObjectPooler RocketBullPooler;
    public GameObject RocketBull;

    GameObject DefensiveBullPool;
    ObjectPooler DefensiveBullPooler;
    public GameObject DefensiveBull;

    public float speed;
    public float health;
    float maxHealth;
    public float secondsPerShot;
    public float projectileSpeed;
    public int points;
    public int maxPoints = 0;
    public float damage;
    float timer;
    Slider special;
    Slider healthbar;
    public GameObject projectile;
    public GameObject ulti;

    Vector3 movement = new Vector3();
    float h;
    float v;
    public bool isDead = false;

    private void Start() {
        special = GameObject.FindGameObjectWithTag("ChargeBar").GetComponent<Slider>();
        healthbar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        projectile.GetComponent<Projectile>().damage = damage;
        bulletPool = GameObject.FindGameObjectWithTag("PlayerBulletPool");
        bulletPooler = bulletPool.GetComponent<ObjectPooler>();
        projectile = bulletPooler.pooledObject;

        PlasmaBullPool = GameObject.FindGameObjectWithTag("PlasmaBullPool");
        PlasmaBullPooler = PlasmaBullPool.GetComponent<ObjectPooler>();
        PlasmaBull = PlasmaBullPooler.pooledObject;

        FlatBullPool = GameObject.FindGameObjectWithTag("FlatBullPool");
        FlatBullPooler = FlatBullPool.GetComponent<ObjectPooler>();
        FlatBull = FlatBullPooler.pooledObject;

        RocketBullPool = GameObject.FindGameObjectWithTag("RocketBullPool");
        RocketBullPooler = RocketBullPool.GetComponent<ObjectPooler>();
        RocketBull = RocketBullPooler.pooledObject;

        LaserBullPool = GameObject.FindGameObjectWithTag("LaserBullPool");
        LaserBullPooler = LaserBullPool.GetComponent<ObjectPooler>();
        LaserBull = LaserBullPooler.pooledObject;

        DefensiveBullPool = GameObject.FindGameObjectWithTag("DefensiveBullPool");
        DefensiveBullPooler = DefensiveBullPool.GetComponent<ObjectPooler>();
        DefensiveBull = DefensiveBullPooler.pooledObject;
        maxHealth = health;
    }


    void FixedUpdate()
    {
        SpecialAttack();
        Move();
        FireAtSpecifiedRate();
        PlasmaAttack();
        LaserAttack();
        Defence();
    }

    void FireAtSpecifiedRate()
    {
        timer += Time.deltaTime;
        x3Timer += Time.deltaTime;
        rocketTimer += Time.deltaTime;
        flattimer += Time.deltaTime;
        if (timer > secondsPerShot)
        {
            Fire();
            timer = 0;
        }
        if (x3Activated)
        {
            if (x3Timer > 10)
                x3Activated = false;
        }
        if (rocketActivated)
        {
            if(rocketTimer > 15)
            {
                rocketActivated = false;
            }
        }
        if (flatactivated)
        {
            if(flattimer > 7)
            {
                flatactivated = false;
            }
        }
    }

    void FlatAttack()
    {
        Vector3 position1 = transform.position + new Vector3(0.5f, 0.5f);
        GameObject bull01 = FlatBullPooler.GetPooledObject(position1, transform.rotation);
        bull01.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, -1.3f);
        bull01.GetComponent<FlatBull>().damage = damage;
        Vector3 position2 = transform.position + new Vector3(0.5f, -0.5f);
        GameObject bull02 = FlatBullPooler.GetPooledObject(position2, transform.rotation);
        bull02.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 1.3f);
        bull02.GetComponent<FlatBull>().damage = damage;
    }

    void SpecialAttack() {

        if(Input.GetKeyDown("space") && special.value >= 100) {
            ulti.SetActive(true);
            special.value -= 100;
        }
    }

    void Defence()
    {
        if (defenceActivated)
        {
            defenceTimer += Time.deltaTime;
            if (defenceSpawn)
            {
                Vector3 position1 = transform.position + new Vector3(0.8f, 0);
                DefensiveBull = DefensiveBullPooler.GetPooledObject(transform.localPosition, transform.rotation);
                defenceSpawn = false;
            }
            DefensiveBull.transform.localPosition = transform.localPosition + new Vector3(0.8f, 0);
            DefensiveBull.GetComponent<DefensiveBull>().damage = damage;
            if (defenceTimer > 7)
            {
                defenceActivated = false;
                DestroyObject(DefensiveBull);
            }
        }
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        movement.Set(h, v, 0);
        transform.localPosition += movement * speed;
        float newX = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
        float newY = Mathf.Clamp(transform.position.y, -4, 4);
        transform.localPosition = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Projectile missile = collision.gameObject.GetComponent<Projectile>();
            PlasmaBull missile2 = collision.gameObject.GetComponent<PlasmaBull>();
            health -= missile.GetDamage();
            healthbar.value -= missile.GetDamage();
            missile.Hit();
            health -= missile2.GetDamage();
            healthbar.value -= missile2.GetDamage();
            missile2.Hit();
        } else if (collision.gameObject.tag == "Boss") {
            health -= 300;
            healthbar.value -= 300;  
        } else if (collision.gameObject.tag == "Enemy")
        {
            health -= 70;
            healthbar.value -= 70;
            IncreasePoints(collision.gameObject.GetComponent<EnemyBehaviour>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Enemy2")
        {
            Enemy2Behaviour enemy = collision.gameObject.GetComponent<Enemy2Behaviour>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<Enemy2Behaviour>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Enemy3")
        {
            collision.gameObject.GetComponent<EnemyBehaviour3>();
            health = 0;
            healthbar.value = 0;
        }
        else if (collision.gameObject.tag == "Enemy4")
        {
            EnemyBehaviour4 enemy = collision.gameObject.GetComponent<EnemyBehaviour4>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<EnemyBehaviour4>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Eenemy")
        {
            Eenemy enemy = collision.gameObject.GetComponent<Eenemy>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<Eenemy>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Eenemy2")
        {
            Eenemy2 enemy = collision.gameObject.GetComponent<Eenemy2>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<Eenemy2>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Eenemy3")
        {
            Eenemy3 enemy = collision.gameObject.GetComponent<Eenemy3>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<Eenemy3>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Eenemy4")
        {
            Eenemy4 enemy = collision.gameObject.GetComponent<Eenemy4>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<Eenemy4>().pointsDropped);
        }
        else if (collision.gameObject.tag == "Eenemy5")
        {
            Eenemy5 enemy = collision.gameObject.GetComponent<Eenemy5>();
            health = 0;
            healthbar.value = 0;
            IncreasePoints(collision.gameObject.GetComponent<Eenemy5>().pointsDropped);
        }
        else if (collision.gameObject.tag == "HealthUp")
        {
            health += 50;
            healthbar.value += 50;
            if (health > maxHealth)
                health = maxHealth;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "TripleShot")
        {
            x3Activated = true;
            rocketActivated = false;
            x3Timer = 0;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Drop2")
        {
            rocketActivated = true;
            x3Activated = false;
            rocketTimer = 0;
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "Drop3")
        {
            if (defenceActivated)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                defenceActivated = true;
                defenceSpawn = true;
                defenceTimer = 0;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Drop4")
        {
            flatactivated = true;
            x3Activated = false;
            flattimer = 0;
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "BombBull")
        {
            health = 0;
            healthbar.value = 0;
            collision.gameObject.GetComponent<BombBull>().Destroy();
        }
        else if (collision.gameObject.tag == "PowerUp")
        {
            if (points >= 1000)
            {
                GameObject pwup = collision.gameObject;
                healthbar.maxValue *= pwup.GetComponent<Powerup>().Health;
                maxHealth *= pwup.GetComponent<Powerup>().Health;
                secondsPerShot *= pwup.GetComponent<Powerup>().FiringRate;
                damage *= pwup.GetComponent<Powerup>().Damage;

                //FindObjectOfType<Shop>().DestroyPowerUps();
                //pwup.GetComponent<Powerup>().Hit();
                IncreasePoints(-1000);
                pwup.GetComponent<Powerup>().Hit();
            }
            
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }
        
    }

    public void IncreasePoints(int pointsAdded)
    {
        points += pointsAdded;
        if(pointsAdded > 0)
            maxPoints += pointsAdded;
    }
    private void Fire()
    {
        if(x3Activated)
        {
            FireThree();
            return;
        }
        if (rocketActivated)
        {
            RocketAttack();
            return;
        }
        if (flatactivated)
        {
            FlatAttack();
            return;
        }
        Vector3 position = transform.position + new Vector3(0.8f, 0f);
        GameObject missile = bulletPooler.GetPooledObject(position, transform.rotation);
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile.GetComponent<Projectile>().damage = damage;
        //source.PlayOneShot(laserSound, 100);
    }
    void PlasmaAttack()
    {
        if (Input.GetKeyDown("z") && special.value >= 5)
        {
            Vector3 position1 = transform.position + new Vector3(0.5f, 0.5f);
            GameObject bull01 = PlasmaBullPooler.GetPooledObject(position1, transform.rotation);
            bull01.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
            bull01.GetComponent<PlasmaBull>().damage = damage;
            Vector3 position2 = transform.position + new Vector3(0.5f, -0.5f);
            GameObject bull02 = PlasmaBullPooler.GetPooledObject(position2, transform.rotation);
            bull02.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
            bull02.GetComponent<PlasmaBull>().damage = damage;
            special.value -= 5;
        }
    }
    void RocketAttack()
    {
        Vector3 position1 = transform.position + new Vector3(0.8f, 0);
        GameObject bull01 = RocketBullPooler.GetPooledObject(position1, transform.rotation);
        bull01.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        bull01.GetComponent<RocketBull>().damage = damage;
    }
    void LaserAttack()
    {
        if (Input.GetKeyDown("x") && special.value >= 100)
        {
            Vector3 position1 = transform.position + new Vector3(0.8f, 0f);
            GameObject bull01 = LaserBullPooler.GetPooledObject(position1, transform.rotation);
            bull01.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
            bull01.GetComponent<LaserBull>().damage = damage;
            special.value -= 100;
        }
    }
    private void FireThree()
    {
        Vector3 position1 = transform.position + new Vector3(0.5f, 0.5f);
        Vector3 position2 = transform.position + new Vector3(0.5f, -0.5f);
        Vector3 position3 = transform.position + new Vector3(0.8f, 0f);
        GameObject missile1 = bulletPooler.GetPooledObject(position1, transform.rotation);
        missile1.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile1.GetComponent<Projectile>().damage = damage;
        GameObject missile2 = bulletPooler.GetPooledObject(position2, transform.rotation);
        missile2.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile2.GetComponent<Projectile>().damage = damage;
        GameObject missile3 = bulletPooler.GetPooledObject(position3, transform.rotation);
        missile3.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed, 0f);
        missile3.GetComponent<Projectile>().damage = damage;
    }

}
