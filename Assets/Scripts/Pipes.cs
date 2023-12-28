using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float _velocity;
    private float leftEdge;

    // Start is called before the first frame update
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += ((Vector3.left * _velocity) * Time.deltaTime);
        if (transform.position.x <= leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
