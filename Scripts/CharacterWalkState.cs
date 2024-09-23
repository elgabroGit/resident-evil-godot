using Godot;
using System;

public partial class CharacterWalkState : CharacterMovementState
{

    public override void _Input(InputEvent @event)
    {
        if (characterNode.ControllerNode.run)
        {
            characterNode.StateMachineNode.SwitchState<CharacterRunState>();
        }
    }

    protected override void EnterState()
    {
        base.EnterState();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_WALK);
    }
}
