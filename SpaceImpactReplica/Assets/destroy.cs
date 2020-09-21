using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class destroy : MonoBehaviour {

    float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 1)
            Destroy();
	}
    public void Destroy()
    {
        timer = 0;
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        gameObject.SetActive(false);
    }
}
