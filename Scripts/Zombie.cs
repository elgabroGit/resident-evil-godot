using Godot;
using System;

public partial class Zombie : Entity
{   
    [Export] public int baseGrabDamage = 5;
    public override void TakeDamage(Weapon weapon)
    {
        damageReceived = weapon.damage;
        StateMachineNode.SwitchState<EnemyShotState>();
        GD.Print(HealthValue);
    }

    public override void Die()
    {
        StateMachineNode.SwitchState<EnemyDeathState>();
    }

    public override void _Process(double delta)
    {
        if(HealthValue <= 0)
        {
            Die();
        }
    }

}
