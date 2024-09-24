using Godot;
using System;

public partial class Zombie : Entity
{   
    public override void TakeDamage(Weapon weapon)
    {
        GD.Print("Colpito");
        damageReceived = weapon.damage;
        StateMachineNode.SwitchState<EnemyShotState>();
    }

    public override void Die()
    {
         StateMachineNode.SwitchState<EnemyDeathState>();
    }
}
