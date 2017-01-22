using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LevelState : NetworkBehaviour {
    public static LevelState singleton;

    public BouncingBall spawnedBall;
    public ForceArea spawnedWave;

    [SyncVar]
    public bool gameActive = false;

    [SyncVar]
    public bool roundStarted = true;

    public int roundNumber;
    public float secondsPassedInRound;

    public int teamLeftScore;
    public int teamRightScore;

    public List<BouncingBall> balls = new List<BouncingBall>();
    public List<ForceArea> waves = new List<ForceArea>();

    public float secondsUntilNextBall;

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
        if(gameActive)
        {
            if(roundStarted)
            {
                if(secondsUntilNextBall <= 0)
                {
                    SpawnBall();
                    secondsUntilNextBall = 5.0f;
                }

                secondsPassedInRound += Time.smoothDeltaTime;
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
        }
    }

    public void StartNewGame()
    {
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
        if(winningTeam == PlayerTeam.Left)
        {
            teamLeftScore++;
        }
        else if(winningTeam == PlayerTeam.Right)
        {
            teamRightScore++;
        }

        print("Team " + winningTeam.ToString() + " scored (" + teamLeftScore + " - " + teamRightScore + ")");

        StartNextRound();
    }

    public void ClearLevelElements()
    {
        print("Cleared level elements");
        foreach(BouncingBall ball in balls)
        {
            Object.Destroy(ball.gameObject);
        }
        balls.Clear();

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
