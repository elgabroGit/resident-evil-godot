using Godot;
using System;

public partial class CharacterRunState : CharacterMovementState
{

    public override void _Input(InputEvent @event)
    {
        
        if(!characterNode.ControllerNode.run)
        {
            characterNode.StateMachineNode.SwitchState<CharacterWalkState>();
        }
    }

    protected override void EnterState()
	{
		base.EnterState();
		characterNode.AnimPlayerNode.Play(GameConstants.ANIM_RUN);
	}

}
