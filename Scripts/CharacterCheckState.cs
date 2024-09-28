using Godot;
using System;

public partial class CharacterCheckState : State
{
    protected override void EnterState()
    {
        GD.Print("Enter Check State");

        if(characterNode.CheckBox.HasOverlappingBodies())
        {
            Item item = (Item) characterNode.CheckBox.GetOverlappingBodies()[0];
            GD.Print(item.itemName);
            DelegateItemLogic(item);
            
        }
        else
        {
            characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
        }

    }

    public void DelegateItemLogic(Item item)
    {
        if(item.isPickable)
        {
            Character player = (Character) characterNode;
            player.itemToPick = item;
            characterNode.StateMachineNode.SwitchState<CharacterPickState>();
        }else{
            item.ReadDescription();
            characterNode.StateMachineNode.SwitchState<CharacterIdleState>();
        }
    }
}
