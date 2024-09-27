using Godot;
using System;
using System.Linq;

public partial class EnemyShotState : State
{
    [Export] private int staggerHits = 3;
    private static int counter = 0;
    [Export] private AudioStreamPlayer3D[] audios;
    [Export] private Timer staggerReset;

    public override void _Ready()
    {
        base._Ready();
    }

    protected override void EnterState()
    {
        var random = new RandomNumberGenerator();
        
        staggerReset.Start();
        
        // Todo: Genera un errore la prima volta che viene chiamato.
        staggerReset.Timeout -= ResetStagger;
        
        staggerReset.Timeout += ResetStagger;
        random.Randomize();
        audios[random.RandiRange(0,audios.Length - 1)].Play();
        characterNode.HealthValue -= characterNode.damageReceived;

        if(((Zombie) characterNode).weaponDamager.fireType != GameConstants.FireType.AUTOMATIC)
        {
            counter++;
        }
        if (counter == staggerHits)
        {
            counter = 0;
            characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
            characterNode.AnimPlayerNode.Stop();
            characterNode.AnimPlayerNode.Play("damage");
        }
        else
        {
            
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
    }

    private void ResetStagger()
    {
        counter = 0;
        GD.Print("Resetted");
    }


    private void HandleAnimationFinished(StringName animName)
    {
        if (animName == "damage")
        {
            characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
    }


}
