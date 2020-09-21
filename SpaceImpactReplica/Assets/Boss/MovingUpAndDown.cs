using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpAndDown : MonoBehaviour {

    public float width = 13f;
    public float speed = 5f;
    private float ymin, ymax;
    private bool moveUp = false;

    void Start() {

        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 upEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distanceToCamera));
        ymin = bottomEdge.x;
        ymax = upEdge.x;
    }
    void FixedUpdate() {
        if (moveUp) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.y + (0.5f * width);
        float leftEdgeOfFormation = transform.position.y - (0.5f * width);
        if (leftEdgeOfFormation < ymin) {
            moveUp = true;
        } else if (rightEdgeOfFormation > ymax) {
            moveUp = false;
        }
    }
}
