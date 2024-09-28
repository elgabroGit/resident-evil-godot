using Godot;
using System;

public partial class Weapon : Node3D
{
    [ExportGroup("Weapon Stat")]
    [Export] public int damage = 1;
    [Export] public int magazineAmount = 7;
    [Export] public GameConstants.FireType fireType = GameConstants.FireType.MANUAL;
    [Export] public GameConstants.AmmoType ammoType = GameConstants.AmmoType.GUN;

    [ExportGroup("Weapon Local Data")]
    [Export] public MeshInstance3D meshInstance3D;
    [Export] public AudioStreamPlayer3D audioStreamPlayer3D;
    [Export] public RayCast3D rayCast3D;
    [Export] public Timer reloadTimer;
    [Export] public string animationShot;
    [Export] public string animationIdle;    
}
