using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    public float Health;
    public float FiringRate;
    public float Damage;
    public bool Triggerred = false;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * 1f);
        if (isOffScreen())
            Destroy(gameObject);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    bool isOffScreen()
    {
        if (transform.position.x < -10 )
            return true;
        return false;
    }
}
