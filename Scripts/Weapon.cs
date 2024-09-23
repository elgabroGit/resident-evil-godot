using Godot;
using System;

public partial class Weapon : Node3D
{
    [Export] public MeshInstance3D meshInstance3D;
    [Export] public AudioStreamPlayer3D audioStreamPlayer3D;
    [Export] public RayCast3D rayCast3D;
    [Export] public Timer reloadTimer;
    [Export] public string animationShot;
    [Export] public string animationIdle;
    
}
