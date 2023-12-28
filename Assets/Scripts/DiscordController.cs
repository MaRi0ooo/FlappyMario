using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Discord;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DiscordController : MonoBehaviour
{
    [SerializeField] private long applicationID;
    [Space]
    [SerializeField] private string _details = "Walking around the world";
    [SerializeField] private string _state = "Current/Best Score:";
    [Space]
    [SerializeField] private string largeImage = "star";
    [SerializeField] private string largeText = "FlappyMario";

    private long _time;

    [SerializeField] Text scoreText;
    [SerializeField] Text bestScore;

    private static bool instanceExists;
    public Discord.Discord _discord;

    private void Awake()
    {
        // Transition the GameObject between scenes, destroy any duplicates
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);
        _time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy the GameObject if Discord isn't running
        try
        {
            _discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        // Update Status every frame
        try
        {
            var activityManager = _discord.GetActivityManager();
            var _activity = new Discord.Activity()
            {
                Details = _details,
                State = _state + $" {scoreText.text} | {bestScore.text}",
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText,
                },
                Timestamps =
                {
                    Start = _time
                }
            };

            activityManager.UpdateActivity(_activity, (res) =>
            {
                if (res != Discord.Result.Ok)
                {
                    Debug.LogWarning("Failed connecting to Discord!");
                }
            });
        }
        catch
        {
            // If updating the status falls, Destroy the GameObject
            Destroy(gameObject);
        }
    }
}
