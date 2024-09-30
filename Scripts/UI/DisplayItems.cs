using Godot;
using System;

public partial class DisplayItems : ItemList
{
    protected MenuInventory mainMenu;
    public override void _Ready()
    {
        base._Ready();
        mainMenu = (MenuInventory) GetParent();
        mainMenu.character.ItemAdded += HandleAddItem;
    }

    private void HandleAddItem(Item newItem)
    {
        AddItem(newItem.itemName, newItem.icon);

    }

}
