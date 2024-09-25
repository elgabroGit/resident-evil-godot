using Godot;
using System;

public partial class EnemyChaseState : State
{
    [Export] private float speed = 5.0f;
    [Export] private Area3D grabArea;
    [Export] private Timer grabCooldown;
    private Entity target = null;

    protected override void EnterState()
	{
		base.EnterState();
		characterNode.AnimPlayerNode.Play("walk");
        characterNode.EnemyDetectionArea.BodyExited += HandlePlayerExited;
        grabArea.BodyEntered += HandleGrab;
	}


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        target = (Entity) characterNode.EnemyDetectionArea.GetOverlappingBodies()[0];

        Vector3 targetPosition = target.GlobalTransform.Origin;
        Vector3 characterPosition = characterNode.GlobalTransform.Origin;

        Vector3 direction = (targetPosition - characterPosition).Normalized();
        characterNode.Velocity = direction * speed * (float)delta * 10.0f;

        characterNode.LookAt(targetPosition);
        characterNode.RotateY(Mathf.Pi);
        characterNode.MoveAndSlide();

        if( grabArea.HasOverlappingBodies() && target == grabArea.GetOverlappingBodies()[0])
        { 
            HandleGrab(grabArea.GetOverlappingBodies()[0]);
        }
    }


    private void HandlePlayerExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
    }

    protected override void ExitState()
	{
		base.ExitState();
        characterNode.EnemyDetectionArea.BodyExited -= HandlePlayerExited;
        grabArea.BodyEntered -= HandleGrab;
	}

    
    private void HandleGrab(Node3D body)
    {
        if(grabCooldown.IsStopped())
        {
            body.GlobalPosition = new Vector3(grabArea.GlobalPosition.X,0,grabArea.GlobalPosition.Z);
            Character player = (Character) body;
            Zombie zombie = (Zombie) characterNode;
            player.damageReceived = zombie.baseGrabDamage;
            player.StateMachineNode.SwitchState<CharacterGrabbedState>();
            characterNode.StateMachineNode.SwitchState<EnemyGrabState>();
        }
    }
}
