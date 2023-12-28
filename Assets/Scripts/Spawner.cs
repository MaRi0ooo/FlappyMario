using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float queueTime = 1.5f;
    [SerializeField] private GameObject _prefab;

    private float _timer;
    [SerializeField] private float minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > queueTime)
        {
            Spawn();
            _timer = 0;
        }
    }

    private void Spawn()
    {
        float _height = Random.Range(minY, maxY);

        GameObject _pipes = Instantiate(_prefab);
        _pipes.transform.position = new Vector2(transform.position.x, _height);
    }
}
