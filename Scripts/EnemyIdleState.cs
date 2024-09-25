using Godot;
using System;

public partial class EnemyIdleState : State
{
    private void HandlePlayerEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    protected override void EnterState()
	{
		base.EnterState();
        characterNode.EnemyDetectionArea.BodyEntered += HandlePlayerEntered;
		characterNode.AnimPlayerNode.Play("idle");
        GD.Print("Enter");
	}

        protected override void ExitState()
	{
		base.ExitState();
		characterNode.EnemyDetectionArea.BodyEntered -= HandlePlayerEntered;
	}
}

