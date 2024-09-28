using Godot;
using System;

public partial class Character : Entity
{
    public Item itemToPick;

    public override void Die()
    {
        StateMachineNode.SwitchState<CharacterDeathState>();
    }

    public void Grabbed()
    {
        StateMachineNode.SwitchState<CharacterGrabbedState>();
    }

    
}
