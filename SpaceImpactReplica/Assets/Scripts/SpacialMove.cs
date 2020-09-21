using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacialMove : MonoBehaviour {

    float timer = 0f;
    public float lastTime = 4f;

    private void Start() {
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer > lastTime) {
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}
