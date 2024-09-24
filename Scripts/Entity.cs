using Godot;
using System;

public abstract partial class Entity : CharacterBody3D
{
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode {get; private set; } 
    [Export] public StateMachine StateMachineNode {get; private set; }
    [Export] public Area3D EnemyDetectionArea {get; private set; }
    [Export] public Controller ControllerNode {get; private set; }

    [ExportGroup("Stats")]
    [Export] public int MaxHealthValue {get; private set; }
    public int HealthValue; 
    public int damageReceived = 0;

    public override void _Ready()
    {
        HealthValue = MaxHealthValue;
    }

    public override void _Process(double delta)
    {
        if(HealthValue <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage(Weapon weapon){}
    public virtual void Die(){}
}
