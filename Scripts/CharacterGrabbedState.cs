using Godot;
using System;

public partial class CharacterGrabbedState : State
{
    protected override void EnterState()
    {
        base.EnterState();
        GD.Print("Enter grabbed state");
        characterNode.AnimPlayerNode.Play("grabbed");
        characterNode.HealthValue -= characterNode.damageReceived;
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
    }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }
}
