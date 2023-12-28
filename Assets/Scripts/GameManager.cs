using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;

    [SerializeField] GameObject startGame;
    [SerializeField] GameObject scoreUI;

    [SerializeField] GameObject gameOver;
    [SerializeField] Animator _animator;

    public Parallax groundParallax;
    public Parallax cityParallax;

    private int _score;
    private int bestScore;

    [SerializeField] AudioSource audioSourceJump;
    [SerializeField] AudioSource audioSourceGameOver;
    [SerializeField] AudioSource audioSourceBGMusic;

    // Start is called before the first frame update
    public void Start()
    {
        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _player.transform.position = new Vector3(_player.transform.position.x, 1, _player.transform.position.z);

        audioSourceGameOver.Stop();
        audioSourceBGMusic.Play();

        groundParallax.animationSpeed = 0.3f;
        cityParallax.animationSpeed = 0.08f;

        startGame.SetActive(true);
        scoreUI.SetActive(false);
        gameOver.SetActive(false);

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }


    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        _score = 0;
        scoreText.text = _score.ToString();
        audioSourceJump.enabled = true;

        startGame.SetActive(false);
        scoreUI.SetActive(true);

        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        audioSourceJump.enabled = false;
        gameOver.SetActive(true);
        groundParallax.animationSpeed = 0f;
        cityParallax.animationSpeed = 0f;
        _animator.updateMode = AnimatorUpdateMode.Normal;
        Pause();
    }

    public void IncreaseScore()
    {
        _score++;
        scoreText.text = _score.ToString();
        if (bestScore < _score)
        {
            bestScore = _score;
            bestScoreText.text = bestScore.ToString();
        }
    }
}
