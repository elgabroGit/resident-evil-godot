using Godot;
using System;

public partial class EnemyIdleState : State
{
    public override void _Ready()
    {
        base._Ready();
        characterNode.EnemyDetectionArea.BodyEntered += HandlePlayerEntered;
    }

    private void HandlePlayerEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    protected override void EnterState()
	{
		base.EnterState();
		characterNode.AnimPlayerNode.Play("idle");
	}

        protected override void ExitState()
	{
		base.ExitState();
		characterNode.EnemyDetectionArea.BodyEntered -= HandlePlayerEntered;
	}
}

