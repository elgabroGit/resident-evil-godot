using Godot;
using System;

public partial class EnemyDeathState : State
{
    protected override void EnterState()
    {
        base.EnterState();
        GD.Print("Sono Morto");
        characterNode.AnimPlayerNode.Stop();
    }

}
