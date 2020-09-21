using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    public GameObject powerup1;
    public GameObject powerup2;
    public GameObject powerup3;
    GameObject canvas;
    GameObject price;
    float timer;
    int numberOfEnemies;
	// Use this for initialization
	void Start () {
        Instantiate(powerup1, new Vector3(8, 2.5f), powerup1.transform.rotation);
        Instantiate(powerup2, new Vector3(8, 0), powerup1.transform.rotation);
        Instantiate(powerup3, new Vector3(8, -2.5f), powerup1.transform.rotation);
        canvas = GameObject.FindWithTag("Canvas");
        price = getChildGameObject(canvas, "Price");
        price.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer > 17)
        {
            price.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    
    static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

}
