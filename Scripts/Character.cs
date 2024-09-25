using Godot;
using System;

public partial class Character : Entity
{
    public override void Die()
    {
        StateMachineNode.SwitchState<CharacterDeathState>();
    }
}
