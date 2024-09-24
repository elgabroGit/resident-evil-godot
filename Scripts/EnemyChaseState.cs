using Godot;
using System;

public partial class EnemyChaseState : State
{

    protected override void EnterState()
	{
		base.EnterState();
		characterNode.AnimPlayerNode.Play("walk");
        characterNode.EnemyDetectionArea.BodyExited += HandlePlayerExited;
	}

    private void HandlePlayerExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
    }

}
