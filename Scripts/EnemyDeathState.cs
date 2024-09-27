using Godot;
using System;

public partial class EnemyDeathState : State
{
    [Export] public AudioStreamPlayer3D deathSound;
    [Export] public CollisionShape3D collisionShape;
    [Export] public Timer despawnTimer;

    protected override void EnterState()
    {
        base.EnterState();
        despawnTimer.Timeout += HandleDespawn;
        despawnTimer.Start();
        characterNode.AnimPlayerNode.Play("die");
        collisionShape.QueueFree();
        deathSound.Play();
    }

    protected override void ExitState()
    {
        despawnTimer.Timeout -= HandleDespawn;
    }

    private void HandleDespawn()
    {
        characterNode.QueueFree();
    }
}
