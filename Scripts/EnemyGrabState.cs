using Godot;
using System;

public partial class EnemyGrabState : State
{
    [Export] private Timer grabCooldown;
    [Export] private Area3D grabArea;

    protected override void EnterState()
    {
        
        if(grabCooldown.IsStopped())
        {
            characterNode.AnimPlayerNode.Play("grab");
            grabCooldown.Start();
            characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
            grabCooldown.Timeout += HandleTimeout;
        }
    }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        grabCooldown.Timeout -= HandleTimeout;
    }
    
    private void HandleAnimationFinished(StringName animName)
    {
        Character player = (Character) characterNode.enemyGrabbed; 
        Zombie zombie = (Zombie) characterNode;
        player.damageReceived = zombie.baseGrabDamage;
        player.HealthValue -= player.damageReceived;
        // player.GlobalPosition = characterNode.lastGrabPositionBodySaved;
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    private void HandleTimeout()
    {
        GD.Print("Grab Cooldown Finished");
    }
}
