﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelState : NetworkBehaviour {
    public static LevelState singleton;

    public BouncingBall spawnedBall;
    public GameObject spawnedBallRipples;
    public ForceArea spawnedWave;

    public Text roundNumberText;
    public Text leftTeamScoreText;
    public Text rightTeamScoreText;
    public Text leftTeamSurvivorsNumText;
    public Text rightTeamSurvivorsNumText;

    [SyncVar]
    public bool gameActive = false;

    [SyncVar]
    public bool roundStarted = true;

    public int roundNumber;
    public float secondsPassedInRound;
    public bool roundDecided;
    public float endRoundAtTime;

    public int teamLeftScore;
    public int teamRightScore;

    public List<BouncingBall> balls = new List<BouncingBall>();
    public List<GameObject> ballRipples = new List<GameObject>();
    public List<ForceArea> waves = new List<ForceArea>();

    public float secondsUntilNextBall;

    public AudioSource pregameMusic;
  public AudioSource playerDie;
  public AudioSource bounce;
  // Use this for initialization
  void Awake ()
    {
        singleton = this;
        InitializeGame();
	}

    void Start()
    {
    }
	
	void Update ()
    {
        if(Input.GetKey("left shift") && Input.GetKeyDown("s"))
        {
            StartNewGame();
        }

        if(roundNumber > 0)
        {
            roundNumberText.text = "Round " + roundNumber;
        }
        else
        {
            roundNumberText.text = "Waiting for players...";
        }

        leftTeamScoreText.text = teamLeftScore.ToString();
        rightTeamScoreText.text = teamRightScore.ToString();
        leftTeamSurvivorsNumText.text = GetPlayersAliveLeftTeam().ToString();
        rightTeamSurvivorsNumText.text = GetPlayersAliveRightTeam().ToString();

        if(gameActive)
        {
            if(roundStarted && !roundDecided)
            {
                if(secondsUntilNextBall <= 0)
                {
                    SpawnBall();
                    secondsUntilNextBall = 5.0f;
                }

                secondsUntilNextBall -= Time.smoothDeltaTime;

                if(GetPlayersAliveLeftTeam() <= 0 && GetPlayersAliveRightTeam() <= 0)
                {
                    FinishRound(PlayerTeam.Undeclared); // Draw
                }
                else if(GetPlayersAliveRightTeam() <= 0)
                {
                    FinishRound(PlayerTeam.Left);
                }
                else if(GetPlayersAliveLeftTeam() <= 0)
                {
                    FinishRound(PlayerTeam.Right);
                }
            }

            if(roundStarted && roundDecided)
            {
                if(secondsPassedInRound >= endRoundAtTime)
                {
                    StartNextRound();
                }
            }

            secondsPassedInRound += Time.smoothDeltaTime;
        }
    }

    public void StartNewGame()
    {
        gameActive = true;
        StartCoroutine(FadeOut.fadeOut(pregameMusic, 1f));

        print("Starting new game");
        InitializeGame();
        StartNextRound();
    }

    public void InitializeGame()
    {
        roundNumber = 0;
        teamLeftScore = 0;
        teamRightScore = 0;
    }

    public void StartNextRound()
    {
        roundNumber++;
        secondsPassedInRound = 0;
        roundDecided = false;
        endRoundAtTime = -1;

        ClearLevelElements();
        RevivePlayers();

        roundStarted = true;

        secondsUntilNextBall = 3.0f;

        if(roundNumber == 1)
        {
        }
        else
        {
        }

        print("Starting round " + roundNumber);
    }

    public void FinishRound(PlayerTeam winningTeam)
    {
        if(!roundDecided)
        {
            roundDecided = true;

            ScreenShaker.singleton.AddShake(2.0f, 4.0f);

            if(winningTeam == PlayerTeam.Left)
            {
                teamLeftScore++;
            }
            else if(winningTeam == PlayerTeam.Right)
            {
                teamRightScore++;
            }

            print("Team " + winningTeam.ToString() + " scored (" + teamLeftScore + " - " + teamRightScore + ")");

            endRoundAtTime = secondsPassedInRound + 3.0f;
        }
    }

    public void ClearLevelElements()
    {
        print("Cleared level elements");
        foreach(BouncingBall ball in balls)
        {
            Object.Destroy(ball.gameObject);
        }
        balls.Clear();

        foreach(GameObject ripple in ballRipples)
        {
            Object.Destroy(ripple.gameObject);
        }
        ballRipples.Clear();

        foreach(ForceArea wave in waves)
        {
            Object.Destroy(wave.gameObject);
        }
        waves.Clear();
    }

    public void RevivePlayers()
    {
        print("Revived players");
        foreach(PlayerUnit player in Server.singleton.playerUnits.Values)
        {
            player.Revive();
        }
    }

    public void SpawnBall()
    {
        print("Spawned ball");
        BouncingBall newBall = Instantiate(spawnedBall) as BouncingBall;
        balls.Add(newBall);

        print("Spawned ball ripple particles");
        GameObject newRipples = Instantiate(spawnedBallRipples) as GameObject;
        ballRipples.Add(newRipples);
        newRipples.GetComponent<StickToObject>().target = newBall.gameObject;
    }

    public int GetPlayersAliveLeftTeam()
    {
        int result = 0;
        foreach(int playerID in Server.singleton.leftTeamPlayerIDs)
        {
            PlayerUnit player = Server.singleton.playerUnits[playerID];
            if(player.alive)
            {
                result++;
            }
        }

        return result;
    }

    public int GetPlayersAliveRightTeam()
    {
        int result = 0;
        foreach(int playerID in Server.singleton.rightTeamPlayerIDs)
        {
            PlayerUnit player = Server.singleton.playerUnits[playerID];
            if(player.alive)
            {
                result++;
            }
        }

        return result;
    }
}
