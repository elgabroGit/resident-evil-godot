using Godot;
using System;

public partial class DisplayItems : GridContainer
{
    [Export] Character character;

    public override void _Ready()
    {
        base._Ready();
        character.ItemAdded += HandleAddItem;
    }

    private void HandleAddItem(Item newItem)
    {
        Label labelItem = new Label();
        labelItem.Text = newItem.itemName;
        AddChild(labelItem);
        // TextureRect texture = new();
        // texture.Texture = newItem.icon;
        // texture.Size = new Vector2(32,32);
        // AddChild(texture);
    }

}
