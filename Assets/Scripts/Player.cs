using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 4f;
    private Rigidbody2D rb;

    AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceBGMusic;
    [SerializeField] private AudioSource audioSourceGameOver;

    [SerializeField] private float tiltAngle = 110f;
    [SerializeField] private float fallMultiplier = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * jumpForce;
            audioSource.Play();
        }

        if (rb.velocity.y > -31)
        {
            float _angle = Mathf.Lerp(-31, -tiltAngle, -rb.velocity.y / 10);
            transform.rotation = Quaternion.Euler(0, 0, _angle);
            rb.velocity += Vector2.up * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            audioSourceBGMusic.Stop();
            audioSourceGameOver.Play();
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (collision.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
