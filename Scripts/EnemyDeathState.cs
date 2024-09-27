using Godot;
using System;

public partial class EnemyDeathState : State
{
    [Export] public AudioStreamPlayer3D deathSound;
    [Export] public CollisionShape3D collisionShape;
    protected override void EnterState()
    {
        base.EnterState();
        collisionShape.QueueFree();
        deathSound.Play();
        characterNode.AnimPlayerNode.Stop();
    }

}
