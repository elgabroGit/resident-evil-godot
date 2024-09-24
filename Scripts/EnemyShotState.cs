using Godot;
using System;

public partial class EnemyShotState : State
{
    [Export] private int staggerHits = 3;
    private static int counter = 0;

    public override void _Ready()
    {
        base._Ready();
    }

    protected override void EnterState()
    {
        characterNode.HealthValue -= characterNode.damageReceived;
        counter++;
        if (counter == staggerHits)
        {
            counter = 0;
            characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
            characterNode.AnimPlayerNode.Stop();
            characterNode.AnimPlayerNode.Play("damage");
        }
        else
        {
            characterNode.AnimPlayerNode.Play("damage");
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (animName == "damage")
        {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
            characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        }
    }
}
