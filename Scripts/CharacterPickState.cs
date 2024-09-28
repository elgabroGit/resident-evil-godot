using Godot;
using System;

public partial class CharacterPickState : State
{
    private Character player;
    
    protected override void EnterState()
    {
        player = (Character) characterNode;
        //player.itemToPick.ReadDescription();
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;    
        if(player.IsInventoryFull()){
            characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
        }
        else
        {
            characterNode.AnimPlayerNode.Play("pick");
        }

            
    }

    public void ItemToInventory()
    {
        player.AddItem(player.itemToPick);
        player.itemToPick.QueueFree();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        ItemToInventory();
        characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
    }

    protected override void ExitState()
    {  
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }
}
