using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public Vector3 _startPosition;
    private Vector3 pos;
    public float MoveSpeed = 5f;
    float min = 0f;
    float max = 1f;
    private void OnEnable()
    {
        Vector3 tweak = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        _startPosition = transform.position + tweak;
        pos = new Vector3(0, 0, 0);

    }
    void Start()
    {
        Vector3 tweak = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        _startPosition = transform.position + tweak;
    }

    void Update()
    {
        pos += transform.up * Time.deltaTime * MoveSpeed;
        transform.position = pos + _startPosition + new Vector3(0f, Mathf.Sin(Time.time * 2), 0f);
    }
}
