using Godot;
using System;

public partial class CharacterPickState : State
{
    private Character player;
    
    protected override void EnterState()
    {
        player = (Character) characterNode;
        player.itemToPick.ReadDescription();
        characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
    }

    public void RemoveItem()
    {
        player.itemToPick.QueueFree();
    }
}
