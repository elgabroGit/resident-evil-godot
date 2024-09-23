using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }
    
    public Camera3D ActiveCamera { get; set; }

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
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
    }
}
