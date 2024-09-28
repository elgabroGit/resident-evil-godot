using Godot;
using System;

public abstract partial class Item : StaticBody3D{
    [Export] public int id = 0;
    [Export] public String itemName = "Item";
    [Export] public int inventorySpace = 1;
    [Export] public bool isPickable = true;

    [Export(PropertyHint.MultilineText)]
    public string description {set; get;}

    [Export]
    public Texture2D icon {set; get;}

    public void ReadDescription()
    {
        GD.Print(description);
    }
}