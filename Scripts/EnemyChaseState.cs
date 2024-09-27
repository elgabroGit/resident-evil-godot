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
        
        SetPhysicsProcess(true);
		characterNode.AnimPlayerNode.Play("walk");
        characterNode.EnemyDetectionArea.BodyExited += HandlePlayerExited;
        grabArea.BodyEntered += HandleGrab;
        if(target == null){ characterNode.StateMachineNode.SwitchState<EnemyIdleState>(); }

        if(GameManager.Instance.gameState == GameConstants.GameState.PLAYER_DEAD)
        {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }

	}


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if( characterNode.EnemyDetectionArea.HasOverlappingBodies() ){ 
            characterNode.AnimPlayerNode.Play("walk");
            target = (Entity) characterNode.EnemyDetectionArea.GetOverlappingBodies()[0];
        }else{
            characterNode.EnlargeDetectionArea();
            return;
        }

        Vector3 targetPosition = target.GlobalTransform.Origin;
        Vector3 characterPosition = characterNode.GlobalTransform.Origin;

        Vector3 direction = (targetPosition - characterPosition).Normalized();
        characterNode.Velocity = direction * speed * (float)delta * 10.0f;

        characterNode.LookAt(targetPosition);
        characterNode.RotateY(Mathf.Pi);


        if( grabArea.HasOverlappingBodies() && target == grabArea.GetOverlappingBodies()[0])
        { 
            // Sei un cazzo di boccalone...
            HandleGrab(grabArea.GetOverlappingBodies()[0]);
        }

        characterNode.MoveAndSlide();
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
            if( body == null) { return; }
            Character player = (Character) body;
            //characterNode.lastGrabPositionBodySaved = player.GlobalPosition;
            player.GlobalPosition = new Vector3(grabArea.GlobalPosition.X,0,grabArea.GlobalPosition.Z);
            player.LookAt(characterNode.GlobalPosition, Vector3.Up);
            player.RotateY((float) Math.PI);
            characterNode.enemyGrabbed = player;
            player.Grabbed();
            SetPhysicsProcess(false);
            characterNode.StateMachineNode.SwitchState<EnemyGrabState>();
        }
    }
}
