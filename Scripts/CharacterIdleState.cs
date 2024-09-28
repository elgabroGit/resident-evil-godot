using Godot;
using System;

public partial class CharacterIdleState : State
{
	

    public override void _PhysicsProcess(double delta)
    {
        if( characterNode.ControllerNode.direction != Vector2.Zero)
		{
			characterNode.StateMachineNode.SwitchState<CharacterWalkState>();
		}
    }


    public override void _Input(InputEvent @event)
    {
        if(characterNode.ControllerNode.aim)
        {
            characterNode.StateMachineNode.SwitchState<CharacterAimState>();
        }

        if(characterNode.ControllerNode.interact)
        {
            characterNode.StateMachineNode.SwitchState<CharacterCheckState>();
        }
    }

	protected override void EnterState()
	{
		base.EnterState();
		characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
	}

}
