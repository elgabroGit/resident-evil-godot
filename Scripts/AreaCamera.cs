using Godot;
using System;

public partial class AreaCamera : Node3D
{
    private Area3D area3DNode;
    private Camera3D camera3DNode;

    public override void _Ready()
    {
        area3DNode = GetNode<Area3D>("Area3D");
        camera3DNode = GetNode<Camera3D>("Camera3D");
        area3DNode.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        camera3DNode.Current = true;
        GameManager.Instance.ActiveCamera = camera3DNode;
    }
}
