using Godot;
using System;

public partial class CharacterDeathState : State
{
    [Export] public AudioStreamPlayer3D deathSound;
    [Export] public CollisionShape3D collisionShape;
    
    protected override void EnterState()
    {
        base.EnterState();
        characterNode.AnimPlayerNode.Play("die");
        collisionShape.QueueFree();
        deathSound.Play();
    }

}
