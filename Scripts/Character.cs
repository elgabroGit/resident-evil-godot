using Godot;
using System;
using System.Linq;

public partial class Character : Entity
{
    public Item itemToPick = null;
    [Export] public int inventoryMaxSize = 6;
    [Export] public Item[] inventory;
    [Signal] public delegate void ItemAddedEventHandler(Item newItem);
    

    public override void _Ready()
    {
        base._Ready();

    }

    public override void Die()
    {
        StateMachineNode.SwitchState<CharacterDeathState>();
    }

    public void Grabbed()
    {
        StateMachineNode.SwitchState<CharacterGrabbedState>();
    }

    public bool IsInventoryFull()
    {
        if(inventory.Length >= inventoryMaxSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsInventoryEmpty()
    {
        if(inventory.Length <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddItem(Item item)
    {        
        if(IsInventoryFull())
        {
            GD.Print("Inventory Full");
        }
        else
        {
            inventory = inventory.Append(item).ToArray();
            EmitSignal(SignalName.ItemAdded, item);
        }

        GD.Print("---");
        foreach (Item invItem in inventory)
        {
            GD.Print(invItem.itemName);
        }
        GD.Print("---");
    }

    public void RemoveItem(Item item)
    {
        if (inventory.Contains(item))
        {
            var inventoryList = inventory.ToList();
            inventoryList.Remove(item);
            inventory = inventoryList.ToArray();
        }
        else
        {
            GD.Print("Item not found in inventory");
        }
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

    }
}
