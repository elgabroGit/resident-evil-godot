using Godot;
using System;

public abstract partial class CharacterMovementState : State
{
    [Export(PropertyHint.Range, "0,20,0.1")] public float speed = 4;
    [Export(PropertyHint.Range, "0,20,0.1")] public float lerp = 2;


    public override void _PhysicsProcess(double delta)
    {
        if(characterNode.ControllerNode.aim)
        {
            characterNode.ControllerNode.storedMoveDirection = Vector3.Zero;
            characterNode.StateMachineNode.SwitchState<CharacterAimState>();
            return;
        }

        if(characterNode.ControllerNode.direction == Vector2.Zero)
        {
            characterNode.ControllerNode.storedMoveDirection = Vector3.Zero;
            characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
            return;
        }

        if (characterNode.ControllerNode.HasDirectionChanged() || characterNode.ControllerNode.storedMoveDirection == Vector3.Zero)
        {
            UpdateMovementDirection();
        }
        characterNode.Velocity = characterNode.ControllerNode.storedMoveDirection.Normalized() * speed;
        RotateCharacter(delta);
        characterNode.MoveAndSlide();
    }



    private void UpdateMovementDirection()
    {
        Camera3D activeCamera = GameManager.Instance.ActiveCamera;
        Vector3 cameraForward = activeCamera.GlobalTransform.Basis.Z.Normalized();
        Vector3 cameraRight = activeCamera.GlobalTransform.Basis.X.Normalized();
        characterNode.ControllerNode.storedMoveDirection = (cameraRight * characterNode.ControllerNode.direction.X) + (cameraForward * characterNode.ControllerNode.direction.Y);
    }

    private void RotateCharacter(double delta)
    {
        if (characterNode.ControllerNode.storedMoveDirection != Vector3.Zero)
        {
            float targetAngle = Mathf.Atan2(characterNode.ControllerNode.storedMoveDirection.X, characterNode.ControllerNode.storedMoveDirection.Z);
            float currentAngle = characterNode.Rotation.Y;
            float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, (float)delta * lerp);
            characterNode.Rotation = new Vector3(0, smoothAngle, 0);
        }
    }


}
