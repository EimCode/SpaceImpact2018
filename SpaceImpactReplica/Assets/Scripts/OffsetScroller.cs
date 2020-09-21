using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour
{

    float scrollSpeed = -5f;
    Vector2 savedOffset;

    void Start()
    {
        savedOffset = transform.position;
    }

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 19.2f);
        transform.position = savedOffset + Vector2.right * newPos;
    }

    
}