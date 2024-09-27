using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    public GameConstants.GameState gameState;
    
    public Camera3D ActiveCamera { get; set; }

    public Timer deathTimer = new Timer();

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
            gameState = GameConstants.GameState.RUNNING;
            AddChild(deathTimer); 
            deathTimer.WaitTime = GameConstants.TIMER_TO_DEATH;
            deathTimer.OneShot = true;
            deathTimer.Timeout += DeathSequence;
            // Impedisce la distruzione del singleton quando si cambia scena
            SetProcess(true);
        }
        else
        {
            QueueFree();
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(gameState == GameConstants.GameState.PLAYER_DEAD)
        {
            GD.Print("Death Timer Start");
            deathTimer.Start();
            SetProcess(false);
        }
    }

    public void ChangeGameState(GameConstants.GameState newState)
    {
        gameState = newState;
    }

    
    private void DeathSequence()
    {
        ChangeGameState(GameConstants.GameState.GAMEOVER);
        GD.Print("GAMEOVER");
        GetTree().Quit();

    }


}
